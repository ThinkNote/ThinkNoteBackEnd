
namespace ThinkNoteBackEnd.Persistence.Config
{
    public class PersistenceConfigurationModel
    {
        public string RootPath { get; set; }
        public string UserPath { get; set; }
        public string StaticPath { get; set; }
        public string RecognitionPath { get; set; }
    }
    public interface IPersistence
    {

    }
}
