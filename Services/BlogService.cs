using RockstarProj.DBModel;
using RockstarProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RockstarProj.Services
{
    public class BlogService
    {
        private RockstarDBModelDataContext dbContext;

        public List<BlogTbl> ReadAllBlogItems()
        {
            dbContext = new RockstarDBModelDataContext();
            List<BlogTbl> model = (from b in dbContext.BlogTbls
                                   select b).ToList();

            return model;
        }

        public List<BlogFileTbl> ReadAllBlogFiles()
        {
            dbContext = new RockstarDBModelDataContext();
            List<BlogFileTbl> list = (from f in dbContext.BlogFileTbls
                                      select f).ToList();

            return list;
        }

        public void CreateBlogEntry(BlogDataModel model)
        {
            dbContext = new RockstarDBModelDataContext();
            BlogTbl blogTbl = new BlogTbl();
            BlogFileTbl blogFileTbl = new BlogFileTbl();
            DateTime now = DateTime.UtcNow;
            DateTime localNow = TimeZoneInfo.ConvertTimeFromUtc(now, TimeZoneInfo.Local);

            blogTbl.BlogTitle = model.BlogTitle;
            blogTbl.BlogContent = model.BlogContent;
            blogTbl.DateCreated = localNow;

            dbContext.BlogTbls.InsertOnSubmit(blogTbl);
            dbContext.SubmitChanges();

            if (model.FileContent != null)
            {
                byte[] firstUploadFile = new byte[model.FileContent.InputStream.Length];
                model.FileContent.InputStream.Read(firstUploadFile, 0, firstUploadFile.Length);

                blogFileTbl.FileTitle = model.FileContent.FileName;
                blogFileTbl.FileData = firstUploadFile;
                blogFileTbl.BlogId = blogTbl.Id;
                dbContext.BlogFileTbls.InsertOnSubmit(blogFileTbl);
                dbContext.SubmitChanges();
            }
        }

        public void EditBlogRecord(BlogDataModel model)
        {
            dbContext = new RockstarDBModelDataContext();
            BlogTbl oldBlogItem = dbContext.BlogTbls.Where(o => o.Id == model.Id).FirstOrDefault();
            BlogFileTbl oldBlogFile = dbContext.BlogFileTbls.Where(f => f.BlogId == model.Id).FirstOrDefault();
            DateTime now = DateTime.UtcNow;
            DateTime localNow = TimeZoneInfo.ConvertTimeFromUtc(now, TimeZoneInfo.Local);

            oldBlogItem.BlogTitle = model.BlogTitle;
            oldBlogItem.BlogContent = model.BlogContent;
            oldBlogItem.DateCreated = localNow;


            if (model.FileContent != null)
            {
                byte[] firstUploadFile = new byte[model.FileContent.InputStream.Length];
                model.FileContent.InputStream.Read(firstUploadFile, 0, firstUploadFile.Length);

                oldBlogFile.FileTitle = model.FileContent.FileName;
                oldBlogFile.FileData = firstUploadFile;
                oldBlogFile.BlogId = oldBlogItem.Id;
            }


            dbContext.SubmitChanges();
        }





        public BlogTbl ReadBlogItemByID(int id)
        {
            dbContext = new RockstarDBModelDataContext();
            BlogTbl model = (from b in dbContext.BlogTbls
                             where b.Id == id
                             select b).FirstOrDefault();
            return model;
        }


        public BlogDataModel ReadBlogModelItemByID(int id)
        {
            dbContext = new RockstarDBModelDataContext();
            BlogDataModel model = (from b in dbContext.BlogTbls
                                   where b.Id == id
                                   select new BlogDataModel
                                   {
                                       BlogTitle = b.BlogTitle,
                                       BlogContent = b.BlogContent


                                   }).FirstOrDefault();
            return model;
        }


        public void DeleteBlogRecord(int id)
        {
            dbContext = new RockstarDBModelDataContext();

            BlogTbl blogItem = dbContext.BlogTbls.Where(b => b.Id == id).FirstOrDefault();
            List<BlogFileTbl> blogFile = dbContext.BlogFileTbls.Where(f => f.BlogId == id).ToList();
            foreach (BlogFileTbl item in blogFile)
            {
                dbContext.BlogFileTbls.DeleteOnSubmit(item);
                dbContext.SubmitChanges();
            }
            dbContext.BlogTbls.DeleteOnSubmit(blogItem);
            dbContext.SubmitChanges();


        }





    }
}
