using System;
using System.Collections.Generic;

namespace ThinkNoteBackEnd.DAO
{
    public partial class StaticResourceTracker
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string OwnerSchoolCode { get; set; }
        public long? OwnerTeacherUid { get; set; }
        public string OwnerCourseId { get; set; }
        public int? OwnerCourseYear { get; set; }
        public string UploaderType { get; set; }
        public string SourcePath { get; set; }
        public string AssoFileGuid { get; set; }
        public string Filename { get; set; }
        public string ResourceType { get; set; }
    }
}
