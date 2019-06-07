using Multas2.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Multas2.Controllers
{
    [Authorize] // só pessoas autenticadas podem acedes ao seu conteúdo
    public class AgentesController : Controller
    {
        private MultasDB db = new MultasDB();

        // GET: Agentes
        public ActionResult Index()
        {
            //LINQ
            // SELECT * FROM Agentes ORDER BY ID DESC
            var listaDeAgentes = db
                .Agentes
                .OrderByDescending(a=>a.ID)
                .ToList();

            return View(listaDeAgentes);
        }

        // GET: Agentes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                return HttpNotFound();
            }
            // envia os dados do AGENTE para a View
            return View(agentes);
        }

        // GET: Agentes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Esquadra")] Agentes agente,
                                    HttpPostedFileBase fotografia)
        {
            // vars auxiliares
            string caminho = "";
            bool imagemValida = false;

            /// foi fornecido um ficheiro?
            if (fotografia == null) {
                // a foto não existe
                // vou atribuir uma fotografia por defeito
                agente.Fotografia = "nouser.jpg";
            }
            else {
                /// existe ficheiro
                /// é uma imagem (fotografia)?
                /// aceitamos JPEG e PNG
                if (fotografia.ContentType == "image/jpeg" ||
                    fotografia.ContentType == "image/png")
                {
                    /// guardar a imagem e
                    /// - definir um nome
                    Guid g;
                    g = Guid.NewGuid();

                    string extensaoDoFicheiro = Path.
                        GetExtension(fotografia.FileName).
                        ToLower();
                    string nomeFicheiro = g.ToString()+extensaoDoFicheiro;

                    /// - definir um local onde a guardar
                    caminho = Path.Combine(Server.MapPath("~/Imagens/"), nomeFicheiro);

                    /// associar ao agente
                    agente.Fotografia = nomeFicheiro;

                    //marca o ficheiro como válido
                    imagemValida = true;
                }
                else
                {
                    // se não é imagem, ou se não existir ficheiro, atribuir imagem por defeito
                    agente.Fotografia = "nouser.jpg";
                }
             
            }
                
            // avalia se os dados fornecidos estão de acordo com o modelo
                if (ModelState.IsValid)
            {
                // adicionar os dados do novo Agente ao Modelo
                db.Agentes.Add(agente);
                try
                {
                    // guardar os dados na BD
                    db.SaveChanges();
                    //guardar a imagem do disco rígido do servidor
                    if (imagemValida) fotografia.SaveAs(caminho);
                    // redireccionar o utilizador para a página de INDEX
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro desconhecido. " +
                        "Pedimos desculpa pela ocorrência");
                }               
            }
            // se cheguei aqui é porque alguma coisa correu mal...
            return View(agente);
        }

        // GET: Agentes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agentes agente = db.Agentes.Find(id);
            if (agente == null)
            {
                return HttpNotFound();
            }
            return View(agente);
        }

        // POST: Agentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Esquadra,Fotografia")] Agentes agente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agente);
        }

        // GET: Agentes/Delete/5
        public ActionResult Delete(int? id)
        {
            /// O ID é nulo se:
            /// - há um erro no programa
            /// - há um 'xico esperto' a tengtar a sua sorte
            /// redirecciono o utilizador para a página de INDEX
            if (id == null)
            {
                return RedirectToAction("Index");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // procura os dados do Agente associado ao ID fornecido
            Agentes agente = db.Agentes.Find(id);

            /// O 'agente' é nulo se:
            /// - há um erro no programa
            /// - há um 'xico esperto' a tengtar a sua sorte
            /// redirecciono o utilizador para a página de INDEX
            if (agente == null)
            {
                return RedirectToAction("Index");
                //return HttpNotFound();
            }
            /// para evitar 'trocas' maliciosas do 'agente'
            /// guardar o ID do agente, para futura comparação
            /// - num cookie cifrado
            /// - numa var de sessão (Não funciona no Asp.Net Core)
            /// - noutro recurso válido...
            Session["IdAgente"] = agente.ID;
            Session["acao"] = "Agentes/Delete";

            // envia para a View os dados do Agente encontrado
            return View(agente);
        }

        // POST: Agentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            /// O ID é nulo se:
            /// - há um erro no programa
            /// - há um 'xico esperto' a tentar a sua sorte
            /// redirecciono o utilizador para a página de INDEX
            if (id == null)
            {
                return RedirectToAction("Index");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //será que o ID do agente que aqui é fornecido é o ID do Agente apresentado no ecrã?
            if (id != (int)Session["IdAgente"] ||
                (string)Session["acao"] != "Agentes/Delete")
            {
                //redirecciono o utilizador para a página de Index
                return RedirectToAction("Index");
            }

            //limpar o valor das var. Sessão, porque não preciso mais delas
            Session["IdAgente"] = "";
            Session["acao"] = "";

            // procura os dados do Agente associado ao ID fornecido
            Agentes agente = db.Agentes.Find(id);
            // Agentes agente = db.Agentes.Find((int)Session["IdAgente"]);

            /// O 'agente' é nulo se:
            /// - há um erro no programa
            /// - há um 'xico esperto' a tengtar a sua sorte
            /// redirecciono o utilizador para a página de INDEX
            if (agente == null)
            {
                return RedirectToAction("Index");
            }

            //Agentes agente = db.Agentes.Find(id);

            try
            {
                //remove os dados do Agente a remover
                db.Agentes.Remove(agente);
                // consolida a remoção da BD
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //Prepara a mensagem de erro a ser enviada para o Utilizador
                ModelState.AddModelError("", "Ocorreu um erro com a remoção do agente: " +
                    agente.Nome +
                    ". Provavelmente existem multas associadas a este agente.");
                return View(agente);
            }
            // redirecciona para a página INDEX
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
