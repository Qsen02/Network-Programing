using HTTP.Requests;
using HTTP.Response;
using Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Controllers
{
   public class HomeController:BaseController
    {
        public IHttpResponse Index(IHttpRequest httpRequest) {
            using (var db = new DatabaseContext()) {
                db.Database.EnsureCreated();
                if (!db.Students.Any())
                {
                    this.ViewData["Students"] = "There are currently no students";
                }
                else {
                    StringBuilder students = new StringBuilder();
                    foreach (var student in db.Students) {
                        students.Append($@"
                        <tr>
                            <td>{student.Id}</td>
                            <td>{student.FirstName}</td>
                            <td>{student.LastName}</td>
                            <td>{student.Year}</td>
                            <td class='text-end'>
                                <a class='btn btn-lg btn-outline-secondary' href='/Home/Update?UpdateId={student.Id}'>
                                    <i class='fas fa-edit'></i>Udpate
                                </a>
                                <a class='btn btn-lg btn-outline-secondary mx-2' data-bs-toggle='modal' href='/Home/Delete?DeleteId={student.Id}'>
                                    <i class='fas fa-trash'></i> Delete
                                </a>
                            </td>
                        </tr>
                        ");
                    }
                    this.ViewData["Students"]=students.ToString();
                }
            }
            return this.View();
        }

        public IHttpResponse Create(IHttpRequest httpRequest) {
            using (var db = new DatabaseContext()) {
                db.Database.EnsureCreated();
                string firstname = httpRequest.FormData["FirstName"].ToString();
                string lastname = httpRequest.FormData["LastName"].ToString();
                int year = int.Parse(httpRequest.FormData["Year"].ToString());

                var student = new Student
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Year = year
                };
                db.Students.Add(student);
                db.SaveChanges();
            }
            return this.Redirect("Index");
        }

        public IHttpResponse Update(IHttpRequest httpRequest) {
                using (var db = new DatabaseContext())
                {
                    if (httpRequest.RequestMethod == HTTP.Enums.HttpRequestMethod.Get) { 
                        int id=int.Parse(httpRequest.QueryData["UpdateId"].ToString());
                        Student student= db.Students.Where(student=>student.Id==id).FirstOrDefault();
                        this.ViewData["Student.Id"]=student.Id.ToString();
                        this.ViewData["Student.FirstName"] = student.FirstName.ToString();
                        this.ViewData["Student.LastName"] = student.LastName.ToString();
                        this.ViewData["Student.Year"] = student.Year.ToString();

                        return this.View();
                    }
                    if (httpRequest.RequestMethod == HTTP.Enums.HttpRequestMethod.Post)
                    {
                        int id = int.Parse(httpRequest.FormData["UpdateId"].ToString());
                        Student student = db.Students.Where(student => student.Id == id).FirstOrDefault();
                        student.FirstName= httpRequest.FormData["FirstName"].ToString();
                        student.LastName = httpRequest.FormData["LastName"].ToString();
                        student.Year = int.Parse(httpRequest.FormData["Year"].ToString());

                        db.Students.Update(student);
                        db.SaveChanges();

                        return this.Redirect("Index");
                    }
                    return this.Redirect("Index");
                }
            }
        public IHttpResponse Delete(IHttpRequest httpRequest)
        {
            using (var db = new DatabaseContext()) {
                db.Database.EnsureCreated();
                int id = int.Parse(httpRequest.QueryData["DeleteId"].ToString());
                Student student = db.Students.Where(student => student.Id == id).FirstOrDefault();
                db.Remove(student);
                db.SaveChanges();
            }
            return this.Redirect("Index");
        }
        }
}
