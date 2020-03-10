namespace evolutionPrueba.Controllers
{
    public class Response<T>
    {
        public T entity { get; set; }
        public bool Status { get; set; }
        public string message { get; set; }

        public Response() { }
        public Response(T entity, bool status, string message)
        {
            this.entity = entity;
            this.Status = status;
            this.message = message;

        }
    }
}