
namespace ThinkNoteBackEnd.Persistence.Config
{
    public class PersistenceConfigurationModel
    {
        public string RootPath { get; set; }
        public string UserPath { get; set; }
        public string StaticPath { get; set; }
        public string RecognitionPath { get; set; }
    }
    public interface IPersistenceService
    {
        //作为持久化服务的标记，以便于依赖注入
    }
}
