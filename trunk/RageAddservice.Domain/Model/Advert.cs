using System;

namespace Rage.Addservice.Domain.Model
{
    //Attachement types:
    // "IMAGE"
    // "VIDEO"

    public class Advert
    {
        public int?     Id      { get; set; }
        public string   Name    { get; set; }
        public string   Description { get; set; }
        public byte[]   Attachment { get; set; }
        public string   Attachment_Type { get; set; }

        public string   Post    { get; set; }

        public Wall      Wall  { get; set; }

        public DateTime CreationTime { get; set; }

        //twitter integration
        public bool UseTwitter { get; set; }
        public int? TweetId { get; set; }
    }
}
