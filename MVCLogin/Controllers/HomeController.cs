using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MVCLogin.Controllers
{
    public class HomeController : Controller
    {
       
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario u)
        {
            if (ModelState.IsValid)
            {
                using (FuncionariosEntities db = new FuncionariosEntities())
                {
                    var v = db.Usuarios.Where(a => a.Usuario1.Equals(u.Usuario1) && a.Senha.Equals(u.Senha)).FirstOrDefault();
                    if(v != null)
                    {
                        Session["LogedUserID"] = v.UserID.ToString();
                        Session["LogedUserNome"] = v.Nome.ToString();
                        return RedirectToAction("AfterLogin");
                    }
                }
            }
            return View(u);
        }
        


        public ActionResult AfterLogin()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
                
            }
            else
            { 
                return RedirectToAction("Index");
            } 
        }
        
        public JsonResult Add_Data(CrudDB cr)
        {
            string msg = string.Empty;
            try
            {
                SqlCommand com = new SqlCommand("AddNewFuncionario", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Nome", cr.Nome);
                com.Parameters.AddWithValue("@Telefone", cr.Telefone);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                msg = "Dados Inseridos.";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                msg = "Erro...!";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            
        }

        public JsonResult Update_Data(CrudDB cr)
        {
            string msg = string.Empty;
            try
            {
                SqlCommand com = new SqlCommand("UpdateFuncionario", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Nome", cr.Nome);
                com.Parameters.AddWithValue("@Telefone", cr.Telefone);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                msg = "Dados Atualizados.";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                msg = "Erro...!";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult Delete_Data(CrudDB cr)
        {
            string msg = string.Empty;
            try
            {
                SqlCommand com = new SqlCommand("Delete_Func", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Nome", cr.Nome);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                msg = "Funcionario Deletado.";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                msg = "Erro...!";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult PopulateData()
        {
            
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("SELECT * FROM Funcionario",con);
                adp.Fill(dt);
            }
            catch (Exception)
            {
                throw;
            }
            return View(dt);
        }


    }

   
}

