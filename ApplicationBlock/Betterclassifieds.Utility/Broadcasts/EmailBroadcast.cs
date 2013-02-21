using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;

namespace Paramount.Betterclassified.Utilities.Broadcast
{
    public class EmailBroadcast:MailMessage
    {

        public EmailBroadcast():base()
        {
            TemplateValue = new Dictionary<string, string>();
            Subject = "None";
        }

        /// <summary>
        /// This is the email template with full path and name
        /// </summary>
        public string TemplateFullName
        {
            get; set;
        }
        
      
        public Dictionary<string, string> TemplateValue
        {
            get; set;
        }

        public  bool Send()
        {  
            bool sucess = false;
            SmtpClient mailClient = new SmtpClient();
            
            try
            { 
                if(string.IsNullOrEmpty(Body))
                {
                    Body = GetContent(ReadTemplateFile(TemplateFullName));
                }
                else
                {
                    Body = GetContent(Body);
                }
                mailClient.Send(this);
                sucess = true;
            }
            catch (Exception ex)
            {
          
            }
           
            return sucess;
        }

        private string GetContent(string templatedContent)
        {
            foreach (var item in TemplateValue)
            {
                templatedContent=templatedContent.Replace(string.Format("[/{0}/]", item.Key), item.Value);
            }
            return templatedContent;
        }

        private static string ReadTemplateFile(string FileName)
        {
            try
            {
                String FILENAME = System.Web.HttpContext.Current.Server.MapPath(FileName);
                StreamReader objStreamReader = File.OpenText(FILENAME);
                String contents = objStreamReader.ReadToEnd();
                return contents;
            }
            catch (Exception ex)
            {

            }
            return "";
        } 

    }
}
