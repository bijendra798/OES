using OESApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OESApi.Controllers
{
    [RoutePrefix("api/student")]
    public class StudentController : ApiController
    {

        onlineEducationEntities db = new onlineEducationEntities();

        [HttpPost]
        [Route("ValidateStudent/{email}/{password}")]
        public IHttpActionResult ValidateStudent(string email, string password)
        {
            //  Teacher teach = new Teacher();
            try
            {
                var student = db.Students.Where(x => x.EmailId == email && x.Password == password).FirstOrDefault();
                if (student != null)
                {
                    student.Lastlogin = System.DateTime.Now;
                    student.LoginAttempt++;
                    db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Ok(student);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            //return teach; 
        }


        [HttpGet]
        [Route("GetAllQuestions")]
        public HttpResponseMessage GetAllQuestions()
        {
            try
            {
                var questions = db.Questions
             .Select(x => new { QuestionId = x.QuestionId, Question = x.Qn, x.Option1, x.Option2, x.Option3, x.Option4 })
             .OrderBy(y => Guid.NewGuid())
             .Take(10)
             .ToArray();


                var updatedQuestions = questions.AsEnumerable()
                         .Select(x => new
                         {
                             QuestionId = x.QuestionId,
                             Qn = x.Question,
                             Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 }
                         }).ToList();
                return this.Request.CreateResponse(HttpStatusCode.OK, updatedQuestions);
            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
                throw ex;
            }
        }


        [HttpGet]
        [Route("GetQuestionsById/{subId}")]
        public HttpResponseMessage GetAllQuestionsById(int subId)
        {
            try
            {
                var questions = db.Questions
             .Where(x => x.SubjectId == subId)
             .Select(x => new { QuestionId = x.QuestionId, Question = x.Qn, subjectId = x.SubjectId, x.Option1, x.Option2, x.Option3, x.Option4 })
             .OrderBy(y => Guid.NewGuid())
             .Take(10)
             .ToArray();


                var updatedQuestions = questions.AsEnumerable()
                         .Where(x => x.subjectId == subId)
                         .Select(x => new
                         {
                             QuestionId = x.QuestionId,
                             Qn = x.Question,
                             Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 }
                         }).ToList();
                return this.Request.CreateResponse(HttpStatusCode.OK, updatedQuestions);
            }
            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
                throw ex;
            }
        }




        [HttpPost]
        [Route("GetAnswers")]
        public HttpResponseMessage GetAnswers(int[] qIds)
        {
            try
            {
                var result = db.Questions.AsEnumerable()
                        .Where(y => qIds.Contains(y.QuestionId))
                        .OrderBy(x => { return Array.IndexOf(qIds, x.QuestionId); })
                        .Select(z => z.CorrectAns)
                        .ToArray();
                return this.Request.CreateResponse(HttpStatusCode.OK, result);
            }


            catch (Exception ex)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound);
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetReportById/{id}")]
        public IEnumerable<object> GetReportById(int id)
        {
            //var report = from i in db.Reports where i.StudentId == id select i;
            var v = from r in db.Reports
                    join s in db.Subjects
                    on r.SubjectId equals s.SubjectId
                    join sn in db.Students on r.StudentId equals sn.StudentId
                    where r.StudentId == id
                    select new
                    {
                        r.Score,
                        r.SubmittedDate,
                        s.SubjectName,
                        sn.RollNo,
                        sn.FirstName,
                        sn.LastName,
                    };
            return v;
        }


        [HttpGet]
        [Route("GetSubjectIdByName/{sname}")]
        public IEnumerable<int> GetSubjectIdByName(string sname)
        {
            try
            {
                //int subject = db.Subjects.Where(x => x.SubjectName == sname).Select()
                var subject = from i in db.Subjects where i.SubjectName == sname select i.SubjectId;

                //int id = Convert.ToInt32(subject);

                if (subject != null)
                {
                    return subject;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [Route("MarkAttendance/{id}")]
        public string MarkAttendance(int id)
        {
            try
            {
                string result;
                var d = System.DateTime.UtcNow.Date;
                var x = db.Attendances.Where(y => y.StudentId == id && y.Date == d).Select(y => y.PresentOrAbsent).FirstOrDefault();
                Attendance attendance = new Attendance();
                if (string.IsNullOrEmpty(x))
                {
                    attendance.StudentId = id;
                    attendance.Date = System.DateTime.Now;
                    attendance.PresentOrAbsent = "Present";
                    db.Attendances.Add(attendance);
                    db.SaveChanges();
                    result = "Attendance Marked";
                    return result;
                }
                else
                {
                    if (x == "Present")
                    {
                        return result = "Attendance already marked";
                    }
                    else
                    {
                        return result = "Student is absent";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        [Route("GetAttendacneById/{id}")]
        public IEnumerable<object> GetAttendacneById(int id)
        {
            try
            {
                //int subject = db.Subjects.Where(x => x.SubjectName == sname).Select()
                var attendance = from i in db.Attendances
                                 join s in db.Students
                                 on i.StudentId equals s.StudentId
                                 where i.StudentId == id
                                 select new
                                 {
                                     i.Date,
                                     i.PresentOrAbsent,
                                     s.FirstName,
                                     s.LastName,
                                     s.RollNo
                                 };


                //int id = Convert.ToInt32(subject);

                if (attendance != null)
                {
                    return attendance;
                }
                else
                {
                    return null;
                }

                //return attendance;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [Route("InsertLastLogin /{date}")]
        public bool InsertLastLogin(DateTime date)
        {
            try
            {
                if (date != null)
                {

                    TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);
                    db.Students.Select(x => x.Lastlogin.Value.Add(ts));
                    //from i in db.Students select i.Lastlogin.Value.Add(ts);
                    db.SaveChanges();

                    return true;

                }
                else
                {
                    return false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("InsertStudentReport/{report}")]
        public bool InsertStudentReport(Report report)
        {
            try
            {
                if (report != null)
                {
                    db.Reports.Add(report);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
