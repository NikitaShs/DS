using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MessagingComminication.MessegingConstans
{
    public class MessagingCommunicationConstans
    {
        public const string EXTENTION_FILE_EVENTS = "file.events";

        public const string EXTENTION_NOTIFICATION_EVENTS = "notification.events";

        public const string EXTENTION_DEPARTAMENTS_EVENTS = "departaments.events";
            
        public const string ROUTING_DEACTIVATED_DEPARTAMENT = "department.deactivated";

        public const string ROUTING_CREATE_DEPARTAMENT = "department.created";

        public const string ROUTING_LOCATION_UPDATE_DEPARTAMENT = "department.locations_updated";

        public const string ROUTING_CREATE_POSITION = "position.created";

        public const string ROUTING_DELETE_POSITION = "position.deleted";

        public const string ROUTING_CREATE_VIDEO = "video.create";

        public const string ROUTING_DELETE_VIDEO = "video.delete";

        public const string ROUTING_CREATE_IMAGE = "image.create";

        public const string ROUTING_DELETE_IMAGE = "image.delete";

        public const string ROUTING_CREATE_AVATAR = "avatar.create";

        public const string ROUTING_DELETE_AVATAR = "avatar.delete";

        public const string ROUTING_CREATE_PREVIEW = "preview.create";

        public const string ROUTING_DELETE_PREVIEW = "preview.delete";
    }
}
