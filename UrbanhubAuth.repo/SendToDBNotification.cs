//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UrbanHub.Data;
//using UrbanHub.Entities;
//using UrbanHub.shared;

//namespace UrbanHubManagement.repo
//{
//    public class SendToDBNotification(UserCard userCard , UrbanHubDbContext context)
//    {
//        public Boolean send( Notification notif)
//        {
//        //public int ID { get; set; }

//        //public String From { get; set; }
//        //public int To { get; set; }

//        //public string? Message { get; set; }
//        //public string? Title { get; set; }
//        //public bool Seen { get; set; } = false;
//        //public DateTime Date { get; set; }
//        var not = new Notification()
//            {
//                ToUserID = notif.ToUserID,
//                Message = notif.Message,
//                Seen = false,
//                Title = notif.Title,
//                From = notif.From,
//                Date = DateOnly.
//        };
//            context.Notifications.Add(not);
//            context.SaveChanges();
//            return true;
//        }
//    }
//}
