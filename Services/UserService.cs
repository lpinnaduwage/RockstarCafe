using RockstarProj.DBModel;
using RockstarProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockstarProj.Services
{
    public class UserService
    {
        private RockstarDBModelDataContext dbContext;


        public void CreateUser(InvolvedModel model)
        {
            dbContext = new RockstarDBModelDataContext();

            RegisteredUser userTbl = new RegisteredUser();
            UserFile fileTbl = new UserFile();

            userTbl.FirstName = model.FirstName;
            userTbl.LastName = model.LastName;
            userTbl.EmailAddress = model.EmailAddress;
            userTbl.PhoneNumber = model.PhoneNumber;
            userTbl.Facebook = model.Facebook;
            userTbl.Twitter = model.Twitter;
            userTbl.LinkedIn = model.LinkedIn;
            userTbl.Instagram = model.Instagram;
            userTbl.Skype = model.Skype;


            userTbl.CompanyProjName = model.CompanyProjName;
            userTbl.CompanyProjDescription = model.CompanyProjDescription;
            userTbl.QuantifiableMetrics = model.QuantifiableMetrics;

            userTbl.ImpactQuestion = model.ImpactQuestion;
            userTbl.ExampleQuestion = model.ExampleQuestion;
            userTbl.LearnQuestion = model.LearnQuestion;
            userTbl.AdditionalInfo = model.AdditionalInfo;

            userTbl.CreatedOn = DateTime.Now;

            dbContext.RegisteredUsers.InsertOnSubmit(userTbl);
            dbContext.SubmitChanges();

            if (model.FirstFile != null)
            {
                byte[] firstUploadFile = new byte[model.FirstFile.InputStream.Length];
                model.FirstFile.InputStream.Read(firstUploadFile, 0, firstUploadFile.Length);
                fileTbl.FileName = model.FirstFile.FileName;
                fileTbl.FileData = firstUploadFile;
                fileTbl.UserID = userTbl.Id;

                dbContext.UserFiles.InsertOnSubmit(fileTbl);
                dbContext.SubmitChanges(); ;
            }


            if (model.SecondFile != null)
            {
                byte[] secondUploadFile = new byte[model.SecondFile.InputStream.Length];
                model.SecondFile.InputStream.Read(secondUploadFile, 0, secondUploadFile.Length);
                fileTbl.FileName = model.FirstFile.FileName;
                fileTbl.UserID = userTbl.Id;
                fileTbl.FileData = secondUploadFile;
            }


            if (model.ThirdFile != null)
            {
                byte[] secondUploadFile = new byte[model.SecondFile.InputStream.Length];
                model.SecondFile.InputStream.Read(secondUploadFile, 0, secondUploadFile.Length);
                fileTbl.FileName = model.FirstFile.FileName;
                fileTbl.UserID = userTbl.Id;
                fileTbl.FileData = secondUploadFile;
            }
        }
    }
}