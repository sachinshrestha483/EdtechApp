using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdtechTest2.Models
{
    public class Content
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // for text lectires articles

        public string LectureArticleText{ get; set; }

        // 

        public string UploadedFileName { get; set; }
        public string Link { get; set; }

        public double Length { get; set; }

        public bool isPreview { get; set; }


        public Lecture Lecture { get; set; }

        public int LectureId { get; set; }

        public string thumbnailImageLink { get; set; }


        public bool  isDownlodableContent { get; set; }


        public enum ContentTypes
        {
           Video,
           Pdf,
          text,
          DownlodableItem
        }

        public ContentTypes ContenType { get; set; }


    }
}
