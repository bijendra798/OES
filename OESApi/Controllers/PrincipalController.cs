using OESApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace OESApi.Controllers
{
    [RoutePrefix("api/principal")]
    public class PrincipalController : ApiController
    {





        onlineEducationEntities OES = new onlineEducationEntities();


        //[Route("AddTeacher")]
        //[ResponseType(typeof(Teacher))]
        //public IHttpActionResult AddTeacher(Teacher teacher)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    OES.Teachers.Add(teacher);
        //    OES.SaveChanges();

        //    return CreatedAtRoute("DeafultApi", new { id = teacher.FirstName }, teacher);

        //}

        [Route("GetAllSubject")]
            public IEnumerable<Subject> GetAllSubject()
            {
                var v = from b in OES.Subjects select b; ;
                return v;
            }

            [Route("GetAllRole")]
            public IEnumerable<UserRole> getallrole()
            {
                var r = from b in OES.UserRoles select b;
                return r;

            }

            [HttpPost]
            [Route("addTeacher/{teacher}")]
            public string AddTeacher(Teacher teacher)
            {
                Teacher teach = new Teacher();
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return "Mdel State Is Invalid" + ModelState;
                    }
                    if (teacher != null)
                    {
                        teach.FirstName = teacher.FirstName;
                        teach.LastName = teacher.LastName;
                        teach.MobileNo = teacher.MobileNo;
                        teach.EmailId = teacher.EmailId;
                        teach.Gender = teacher.Gender;
                        teach.Qualification = teacher.Qualification;
                        teach.SubjectId = teacher.SubjectId;
                        teach.JoiningDate = teacher.JoiningDate;
                        teach.Salary = teacher.Salary;
                        teach.RoleId = teacher.RoleId;
                        teach.Password = teacher.Password;
                        teach.Lastlogin = System.DateTime.Now;
                        teach.LoginAttempts = 0;


                        OES.Teachers.Add(teach);
                        OES.SaveChanges();

                        UserLogin userlogin = new UserLogin();
                        userlogin.Email = teacher.EmailId;
                        userlogin.Password = teacher.Password;
                        userlogin.LastLogin = teacher.Lastlogin;
                        userlogin.LoginAttempt = teacher.LoginAttempts;

                        OES.UserLogins.Add(userlogin);
                        OES.SaveChanges();


                        return teach.FirstName;
                    }
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            [HttpPut]
            [Route("EditTeacher/{teacher}")]
            public string EditTeacher(Teacher teacher)
            {

                try
                {
                    string token;
                    var findTeacher = OES.Teachers.Find(teacher.TeacherId);
                    if (findTeacher != null)
                    {

                        findTeacher.FirstName = teacher.FirstName;
                        findTeacher.LastName = teacher.LastName;
                        findTeacher.MobileNo = teacher.MobileNo;
                        findTeacher.EmailId = teacher.EmailId;
                        findTeacher.Gender = teacher.Gender;
                        findTeacher.Qualification = teacher.Qualification;
                        findTeacher.Salary = teacher.Salary;
                        findTeacher.Password = teacher.Password;

                        OES.Entry(findTeacher).State = System.Data.Entity.EntityState.Modified;
                        OES.SaveChanges();
                        token = "Updated";
                        return token;
                    }
                    else
                    {
                        return token = "Something Went wrong!!";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }



            [HttpGet]
            [Route("GetSubjectIdBySubjectName/{subName}")]
            public int GetSubjectIdBySubjectName(string subName)
            {
                try
                {
                    if (subName != null)
                    {
                        int result = OES.Subjects.Where(x => x.SubjectName == subName).Select(x => x.SubjectId).FirstOrDefault();
                        return result;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }



            [HttpGet]
            [Route("GetAllTeacher")]
            public IQueryable<Teacher> GetAllTeacher()
            {
                return OES.Teachers;
            }

            [HttpGet]
            [Route("GetTeacherWithSubjectName")]
            public IEnumerable<Object> GetTeacherWithSubjectName()
            {
                var result = from teacher in OES.Teachers
                             join subject in OES.Subjects
                             on teacher.SubjectId equals subject.SubjectId
                             select new { teacher, subject.SubjectName };


                return result;
                //// return OES.Teachers;
            }

            [HttpGet]
            [Route("GetRoleIdByRoleName/{roleName}")]
            public int GetRoleIdByRoleName(string roleName)
            {
                try
                {
                    if (roleName != null)
                    {
                        int result = OES.UserRoles.Where(x => x.RoleName == roleName).Select(x => x.RoleId).FirstOrDefault();
                        return result;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            [HttpDelete]
            [Route("DeleteTeacher/{id}")]

            public int DeleteTeacher(int id)
            {
                try
                {
                    int output = 1;
                    var findTeacher = OES.Teachers.Find(id);

                    if (findTeacher != null)
                    {
                        OES.Teachers.Remove(findTeacher);
                        OES.SaveChanges();
                        return output;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


            [HttpPost]
            [Route("AddStudent/{student}")]
            public Student AddStudent(Student student)
            {
                Student stud = new Student();
                try
                {
                    if (student != null)
                    {
                        stud.FirstName = student.FirstName;
                        stud.LastName = student.LastName;
                        stud.MobileNo = student.MobileNo;
                        stud.EmailId = student.EmailId;
                        stud.Gender = student.Gender;
                        stud.RollNo = student.RollNo;
                        stud.ParentsEmailId = student.ParentsEmailId;
                        stud.JoiningDate = student.JoiningDate;
                        stud.ParentsMobileNo = student.ParentsMobileNo;
                        stud.RoleId = student.RoleId;
                        stud.Password = student.Password;
                        stud.Lastlogin = System.DateTime.Now;
                        stud.LoginAttempt = 0;

                        OES.Students.Add(stud);
                        OES.SaveChanges();

                        return stud;
                    }
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }


            [HttpPut]
            [Route("EditStudent/{student}")]
            public string EditStudent(Student student)
            {

                try
                {
                    string token;
                    var findStudent = OES.Students.Find(student.StudentId);
                    if (findStudent != null)
                    {

                        findStudent.FirstName = student.FirstName;
                        findStudent.LastName = student.LastName;
                        findStudent.MobileNo = student.MobileNo;
                        findStudent.EmailId = student.EmailId;
                        findStudent.Gender = student.Gender;
                        findStudent.RollNo = student.RollNo;
                        findStudent.ParentsEmailId = student.ParentsEmailId;
                        findStudent.ParentsMobileNo = student.ParentsMobileNo;
                        //findStudent.ParentsMobileNo = student.Password;

                        OES.Entry(findStudent).State = System.Data.Entity.EntityState.Modified;
                        OES.SaveChanges();
                        token = "Updated";
                        return token;
                    }
                    else
                    {
                        return token = "Something Went wrong!!";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            [HttpDelete]
            [Route("DeleteStudent/{id}")]

            public int DeleteStudent(int id)
            {
                try
                {
                    int output = 1;
                    var findStudent = OES.Students.Find(id);

                    if (findStudent != null)
                    {
                        OES.Students.Remove(findStudent);
                        OES.SaveChanges();
                        return output;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            [HttpGet]
            [Route("GetStudentwithRoles")]
            public IEnumerable<object> GetStudentwithRoles()
            {
                var result = from student in OES.Students
                             join role in OES.UserRoles
                             on student.RoleId equals role.RoleId
                             select new
                             {
                                 student.StudentId,
                                 student.FirstName,
                                 student.LastName,
                                 student.EmailId,
                                 student.Gender,
                                 student.Attendances,
                                 student.RollNo,
                                 student.Password,
                                 student.ParentsMobileNo,
                                 student.ParentsEmailId,
                                 student.JoiningDate,
                                 student.MobileNo,
                                 student.RoleId,
                                 role.RoleName
                             };
                return result;
            }



            [HttpGet]
            [Route("GetPrincipal/{email}/{password}")]
            public IHttpActionResult GetPrincipal(string email, string password)
            {
                var log = OES.Principals.Where(x => x.EmailId == email && x.Password == password).FirstOrDefault();
                if (log != null)
                {
                    return Ok(log);
                }
                else
                    return NotFound();

            }



            [ResponseType(typeof(Principal))]
            public IHttpActionResult GetPrincipal(int id)
            {
                Principal principal = OES.Principals.Find(id);
                if (principal == null)
                {
                    return NotFound();
                }

                return Ok(principal);
            }

            // PUT: api/Login/5
            [ResponseType(typeof(void))]
            public IHttpActionResult PutPrincipal(int id, Principal principal)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != principal.PrincipalId)
                {
                    return BadRequest();
                }

                OES.Entry(principal).State = EntityState.Modified;

                try
                {
                    OES.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrincipalExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return StatusCode(HttpStatusCode.NoContent);
            }

            // POST: api/Login
            [ResponseType(typeof(Principal))]
            public IHttpActionResult PostPrincipal(Principal principal)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                OES.Principals.Add(principal);
                OES.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = principal.PrincipalId }, principal);
            }

            // DELETE: api/Login/5
            [ResponseType(typeof(Principal))]
            public IHttpActionResult DeletePrincipal(int id)
            {
                Principal principal = OES.Principals.Find(id);
                if (principal == null)
                {
                    return NotFound();
                }

                OES.Principals.Remove(principal);
                OES.SaveChanges();

                return Ok(principal);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    OES.Dispose();
                }
                base.Dispose(disposing);
            }

            private bool PrincipalExists(int id)
            {
                return OES.Principals.Count(e => e.PrincipalId == id) > 0;
            }


        }
    }

