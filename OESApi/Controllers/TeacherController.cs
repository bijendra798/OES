using OESApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Data.Entity;

namespace OESApi.Controllers
{
    [RoutePrefix("api/Teacher")]
    public class TeacherController : ApiController
    {
        onlineEducationEntities OES = new onlineEducationEntities();

        [HttpPost]
        [Route("ValidateTeachers/{email}/{password}")]
        public IHttpActionResult ValidateTeachers(string email, string password)
        {
            //  Teacher teach = new Teacher();
            try
            {

                var teach = OES.Teachers.Where(x => x.EmailId == email && x.Password == password).FirstOrDefault();
                if (teach != null)
                {
                    teach.Lastlogin = System.DateTime.Now;
                    teach.LoginAttempts++;
                    OES.Entry(teach).State = System.Data.Entity.EntityState.Modified;
                    OES.SaveChanges();
                    return Ok(teach);
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




        //[HttpPost]
        //[Route("ValidateTeachers/{email}/{password}")]
        //public IHttpActionResult ValidateTeachers(string email, string password)
        //{
        //    //  Teacher teach = new Teacher();
        //    try
        //    {
        //        int n = 0;
        //        switch ()
        //        {
        //            case 1: if (email != null && password != null)
        //            {

        //                var vstudent = OES.Students.Where(x => x.EmailId == email && x.Password == password).FirstOrDefault();
        //                if(vstudent != null)                        
        //            {
        //                vstudent.Lastlogin = System.DateTime.Now;
        //                vstudent.LoginAttempt++;
        //                OES.Entry(vstudent).State = System.Data.Entity.EntityState.Modified;
        //                OES.SaveChanges();
        //                        return Ok(vstudent);                                                
        //            }
        //                    else
        //                    {
        //                        break;
        //                    }
        //            }


        //    }





        //        if ()
        //        {
        //            var vstudent = OES.Students.Where(x => x.EmailId == email && x.Password == password).FirstOrDefault();

        //        ;
        //        }
        //        else if (email != null && password != null)
        //        {
        //            var teach = OES.Teachers.Where(x => x.EmailId == email && x.Password == password).FirstOrDefault();
        //            //teach.Lastlogin = System.DateTime.Now;
        //            //teach.LoginAttempts++;
        //            //OES.Entry(teach).State = System.Data.Entity.EntityState.Modified;
        //            //OES.SaveChanges();
        //            return Ok(teach);
        //        }
        //        else
        //        {
        //            var prince = OES.Principals.Where(x => x.EmailId == email && x.Password == password).FirstOrDefault();
        //            //prince.Lastlogin = System.DateTime.Now;
        //            //prince.LoginAttempt++;
        //            //OES.Entry(prince).State = System.Data.Entity.EntityState.Modified;
        //            //OES.SaveChanges();
        //            return Ok(prince);
        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        throw new Exception(Ex.Message);
        //    }
        //    //return teach; 
        //}



        //[HttpPost]
        //[Route("ValidateTeacher")] 
        //public Teacher ValidateTeacher(Teacher teacher) { 
        //    try { 
        //        Teacher teach; teach = db.Teacher.Where(x => x.EmailId == teacher.EmailId & x.Password == teacher.Password).FirstOrDefault(); 
        //        if (teach != null) 
        //        { 
        //            teacher.Lastlogin = System.DateTime.Now; 
        //            db.Entry(teacher).State = EntityState.Modified; 
        //            db.SaveChanges(); return teach; 
        //        } else 
        //        { return null; 
        //        } 
        //    } 
        //    catch (Exception ex) { 
        //        throw ex; 
        //    } 
        //}


        //[HttpPost]
        //[Route("ValidateAll/{roleId}/{email}/{password}")]
        //public IHttpActionResult ValidateAll(int roleId, string email, string password)
        //{
        //    try
        //    {


        //        switch (roleId)
        //        {
        //            case 1:
        //                if (roleId == 1)
        //                {
        //                    var studentDetails = OES.Students.Where(x => x.EmailId == email && x.Password == password).FirstOrDefault();
        //                    if (studentDetails != null)
        //                    {
        //                        studentDetails.Lastlogin = System.DateTime.Now;
        //                        studentDetails.LoginAttempt++;
        //                        OES.Entry(studentDetails).State =EntityState.Modified;
        //                       OES.SaveChanges();
        //                        return Ok(studentDetails);
        //                    }
        //                    else
        //                    {

        //                        return BadRequest();
        //                    }
        //                }
        //                break;
        //            case 2:
        //                if (roleId == 2)
        //                {
        //                    var teacherDetails = OES.Teachers.Where(x => x.EmailId == email && x.Password == password).FirstOrDefault();
        //                    if (teacherDetails != null)
        //                    {
        //                        teacherDetails.Lastlogin = System.DateTime.Now;
        //                        teacherDetails.LoginAttempts++;
        //                        OES.Entry(teacherDetails).State = EntityState.Modified;
        //                        OES.SaveChanges();
        //                        return Ok(teacherDetails);
        //                    }
        //                    else
        //                    {
        //                        return BadRequest();
        //                    }
        //                }
        //                break;
        //            case 3:
        //                if (roleId == 3)
        //                {
        //                    var princeDetails = OES.Principals.Where(x => x.EmailId == email && x.Password == password).FirstOrDefault();
        //                    if (princeDetails != null)
        //                    {
        //                        princeDetails.Lastlogin = System.DateTime.Now;
        //                        princeDetails.LoginAttempt++;
        //                        OES.Entry(princeDetails).State = EntityState.Modified;
        //                        OES.SaveChanges();
        //                        return Ok(princeDetails);
        //                    }
        //                    else
        //                    {
        //                        return BadRequest();
        //                    }
        //                }
        //                break;

        //            default:
        //                break;
        //        }
        //        return Ok("Nahi yrr");

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        [HttpGet]
        [Route("GetAllReport")]
        public IEnumerable<object> GetAllReport()

        {
            var v = from r in OES.Reports
                    join s in OES.Subjects
                    on r.SubjectId equals s.SubjectId
                    join sn in OES.Students on r.StudentId equals sn.StudentId
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
        [Route("GetStudentResult/{id}")]
        public IEnumerable<Report> GetStudentResult(int id)
        {
            var v = from r in OES.Reports
                    where r.ReportId == id
                    select r;
            return v;
        }


        [HttpGet]
        [Route("GetAttendances")]
        public IEnumerable<object> GetAttendances()
        {
            var attendance = from i in OES.Attendances
                             join j in OES.Students
                             on i.StudentId equals j.StudentId
                             select new
                             {
                                 i.PresentOrAbsent,
                                 i.Date,
                                 j.RollNo,
                                 j.FirstName,
                                 j.LastName
                             };
            return attendance;

        }

        [HttpGet]
        [Route("GetAllTimeTable")]
        public IQueryable<TimeTable> GetAllTimeTable()
        {
            return OES.TimeTables;
        }

        [HttpPost]
        [Route("AddTimetable/{timetable}")]
        public string AddTimeTable(TimeTable timetable)
        {

            TimeTable time = new TimeTable();

            try
            {
                if (!ModelState.IsValid)
                {
                    return "Mdel State Is Invalid" + ModelState;
                }
                if (timetable != null)
                {
                    time.Saturday = timetable.Saturday;

                    time.Sunday = timetable.Sunday;


                    time.Monday = timetable.Monday;


                    time.Tuesday = timetable.Tuesday;


                    time.Wednesday = timetable.Wednesday;

                    time.Thursday = timetable.Thursday;
                    time.Friday = timetable.Friday;
                    time.Time = timetable.Time;







                    OES.TimeTables.Add(time);
                    OES.SaveChanges();

                    return time.Sunday;
               
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    

        [HttpPost]
        [Route("SendMail/{report}")]
        public string SendEmail(Report report)
        {
            try
            {
                //string Pmail = OES.Students.Where(x => x.StudentId == report.StudentId).Select(x => x.ParentsEmailId).FirstOrDefault();
                string score = report.Score.ToString();
                string from = "frankensteinsgame@gmail.com";
                string to = "shubhamnachare08@gmail.com";
                string subjectanme = OES.Subjects.Where(x => x.SubjectId == report.SubjectId).Select(x => x.SubjectName).FirstOrDefault();
                MailMessage message = new MailMessage(from, to);
                message.IsBodyHtml = true;
                message.Subject = "Student Online Test Details";
                message.Body = "Hi<br/><br/>Have a look at your student online test report.<br/>" + "<br/>" +
                    @"<html><head><style type=""text/css"">
            HTML{background-color: #e8e8e8;}
            .courses-table{font-size: 12px; padding: 3px; border-collapse: collapse; border-spacing: 0;}
            .courses-table .description{color: #505050;}
            .courses-table td{border: 1px solid #D1D1D1; background-color: #F3F3F3; padding: 0 10px;}
            .courses-table th{border: 1px solid #424242; color: #FFFFFF;text-align: left; padding: 0 10px;}
            .green{background-color: #6B9852; color:#FFFFFF}
             </style>
            </head> <body> <table > <tr><th class=""green"">Student Id</th><th class=""green"">Subject Name</th><th class=""green"">Score</th><th class=""green"">Date</th></tr><tr><td>" + report.StudentId + "</td><td>" + subjectanme + "</td><td>" + score + "</td><td>" + report.SubmittedDate + "</td></table></body></html>" +
                    "<br/> <br/>Please do not reply to this mail this is System generated mail\n" +
                    "<br/><br/>Thanks & Regards,<br />" + "<br/>Online Education System";
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                //smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential("frankensteinsgame@gmail.com", "Frank@0204");
                //{ 
                //UserName = "frankensteinsgame@gmail.com", Password = "Frank@0204" }; 
                smtpClient.EnableSsl = true;
                //smtpClient.DeliveryMethod=SmtpDeliveryMethod.Network;                      
                smtpClient.Send(message);
                return "Message Sent";
            }
            catch (Exception ex)
            {
                return "Not Send" + ex;
                //throw ex;            
            }
        }

        [HttpPost]
        [Route("SendTimeTable/{timetable}")]
        public string SendTimeTable(TimeTable timetable)
        {
            try
            {

                string from = "frankensteinsgame@gmail.com";
                string to = "shubhamnachare08@gmail.com";

                MailMessage message = new MailMessage(from, to);
                message.IsBodyHtml = true;
                message.Subject = "Student Online Test Details";
                message.Body = "Hi<br/><br/>Time Table.<br/>" + "<br/>" +
                    @"<html><head><style type=""text/css"">
            HTML{background-color: #e8e8e8;}
            .courses-table{font-size: 12px; padding: 3px; border-collapse: collapse; border-spacing: 0;}
            .courses-table .description{color: #505050;}
            .courses-table td{border: 1px solid #D1D1D1; background-color: #F3F3F3; padding: 0 10px;}
            .courses-table th{border: 1px solid #424242; color: #FFFFFF;text-align: left; padding: 0 10px;}
            .green{background-color: #6B9852; color:#FFFFFF}
             </style>
            </head> <body> <table > <tr><th class=""green"">Sunday/th><th class=""green"">Monday</th><th class=""green"">Tuesday</th><th class=""green"">Wednesday</th><th class=""green"">Thursday</th><th class=""green"">Firday</th><th class=""green"">Saturday</th><th class=""green"">Time</th></tr><tr><td>" + timetable.Sunday + "</td><td>" + timetable.Monday + "</td><td>" + timetable.Tuesday + "</td><td>" + timetable.Wednesday + "</td><td>" + timetable.Thursday + "</td><td>" + timetable.Friday + "</td><td>" + timetable.Saturday + "</td><td>" + timetable.Time + "</td></tr></table></body></html>" +
                    "<br/> <br/>Please do not reply to this mail this is System generated mail\n" +
                    "<br/><br/>Thanks & Regards,<br />" + "<br/>Online Education System";
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                //smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential("frankensteinsgame@gmail.com", "Frank@0204");
                //{ 
                //UserName = "frankensteinsgame@gmail.com", Password = "Frank@0204" }; 
                smtpClient.EnableSsl = true;
                //smtpClient.DeliveryMethod=SmtpDeliveryMethod.Network;                      
                smtpClient.Send(message);
                return "Message Sent";
            }
            catch (Exception ex)
            {
                return "Not Send" + ex;
                //throw ex;            
            }
        }




        //public IQueryable<Object> GetTeacher()
        //{
        //    var r = from t in OES.Teachers
        //            join s in OES.Subjects
        //            on t.SubjectId equals s.SubjectId
        //            select new { t, s.SubjectName
        //            };
        //    return r;
        //}

    }
}
