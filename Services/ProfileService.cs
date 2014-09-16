using RockstarProj.DBModel;
using RockstarProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RockstarProj.Services
{
    public class ProfileService
    {
        private RockstarDBModelDataContext dbContext;

        /// <summary>
        /// Returns all User profiles
        /// </summary>
        /// <returns></returns>
        public List<RockstarProfile> ReadAllProfiles()
        {
            dbContext = new RockstarDBModelDataContext();
            List<RockstarProfile> model = (from b in dbContext.RockstarProfiles
                                   select b).ToList();
            return model;
        }

        public List<ProfileData>ReadProfileData()
        {
            dbContext = new RockstarDBModelDataContext();
            List<ProfileData> model = (from b in dbContext.RockstarProfiles
                                       select new ProfileData
                                       {
                                           ProfilePicture = b.ProfilePic.ToArray(),
                                           PopupLayoutImage = b.PopupLayout.ToArray(),
                                           YoutubeLink = b.YoutubeLink
                                       }).ToList();
            return model;
        }


        /// <summary>
        /// Creates a new user profile entry
        /// </summary>
        /// <param name="model"></param>
        /// <param name="file"></param>
        /// <param name="popupFile"></param>
        public void CreateProfileEntry(RockstarProfile model, HttpPostedFileBase file, HttpPostedFileBase popupFile)
        {
            dbContext = new RockstarDBModelDataContext();
            RockstarProfile dbModel = model;
            DateTime now = DateTime.UtcNow;
            DateTime localNow = TimeZoneInfo.ConvertTimeFromUtc(now, TimeZoneInfo.Local);
            if (file != null)
            {
                byte[] fileData = new byte[file.InputStream.Length];
                file.InputStream.Read(fileData, 0, fileData.Length);

                dbModel.ProfilePic = fileData;
            }

            if (popupFile != null)
            {
                byte[] popupData = new byte[popupFile.InputStream.Length];
                popupFile.InputStream.Read(popupData, 0, popupData.Length);

                dbModel.PopupLayout = popupData;
            }

            dbModel.CreatedOn = localNow;
            dbContext.RockstarProfiles.InsertOnSubmit(dbModel);
            dbContext.SubmitChanges();
        }

        /// <summary>
        /// Returns profile by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RockstarProfile ReadProfileByID(int id)
        {
            dbContext = new RockstarDBModelDataContext();
            RockstarProfile model = (from b in dbContext.RockstarProfiles
                             where b.Id == id
                             select b).FirstOrDefault();
            return model;
        }

        public void DeleteBlogRecord(int id)
        {
            dbContext = new RockstarDBModelDataContext();
            RockstarProfile profileItem = dbContext.RockstarProfiles.Where(p => p.Id == id).FirstOrDefault();
            dbContext.RockstarProfiles.DeleteOnSubmit(profileItem);
            dbContext.SubmitChanges();
        }

        /// <summary>
        /// Update profile record
        /// </summary>
        /// <param name="model"></param>
        /// <param name="file"></param>
        /// <param name="popupFile"></param>
        public void EditProfileRecord(RockstarProfile model, HttpPostedFileBase file, HttpPostedFileBase popupFile)
        {
            dbContext = new RockstarDBModelDataContext();
            RockstarProfile oldProfile = dbContext.RockstarProfiles.Where(o => o.Id == model.Id).FirstOrDefault();
            DateTime now = DateTime.UtcNow;
            DateTime localNow = TimeZoneInfo.ConvertTimeFromUtc(now, TimeZoneInfo.Local);

            // Profile parameters
            oldProfile.FirstName = model.FirstName;
            oldProfile.LastName = model.LastName;
            oldProfile.DOB = model.DOB;
            oldProfile.UniversityCollege = model.UniversityCollege;
            oldProfile.Program = model.Program;
            oldProfile.GradYear = model.GradYear;
            oldProfile.Hometown = model.Hometown;
            oldProfile.Organization = model.Organization;
            oldProfile.Title = model.Title;
            oldProfile.Associates = model.Associates;
            oldProfile.Email = model.Email;
            oldProfile.Twitter = model.Twitter;
            oldProfile.Facebook = model.Facebook;
            oldProfile.Phone = model.Phone;
            oldProfile.LinkedIn = model.LinkedIn;
            oldProfile.Instagram = model.Instagram;
            oldProfile.Skype = model.Skype;
            oldProfile.PositiveChangeQuestion = model.PositiveChangeQuestion;
            oldProfile.LearnQuestion = model.LearnQuestion;
            oldProfile.ExampleQuestion = model.ExampleQuestion;
            oldProfile.ImpactQuestion = model.ImpactQuestion;
            oldProfile.AdditonalQuestion = model.AdditonalQuestion;
            oldProfile.YoutubeLink = model.YoutubeLink;

            //File info save
            if (file != null)
            {
                byte[] fileData = new byte[file.InputStream.Length];
                file.InputStream.Read(fileData, 0, fileData.Length);
                oldProfile.ProfilePic = fileData;
            }

            if (popupFile != null)
            {
                byte[] popupData = new byte[popupFile.InputStream.Length];
                popupFile.InputStream.Read(popupData, 0, popupData.Length);

                oldProfile.PopupLayout = popupData;
            }

            oldProfile.CreatedOn = localNow;
            dbContext.SubmitChanges();
        }

        /// <summary>
        /// Returns profile by search text
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public List<RockstarProfile> ProfileSearch(string searchText)
        {
            dbContext = new RockstarDBModelDataContext();
            List<RockstarProfile> list = new List<RockstarProfile>();
            IEnumerable<RockstarProfile> collection = (from p in dbContext.RockstarProfiles
                                                       where p.FirstName == searchText || p.LastName == searchText ||
                                                       p.Hometown == searchText || p.Program == searchText
                                                       select p);
            return collection.ToList();
        }


        
    }
}
