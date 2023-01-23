namespace Domain.DTO
{
    public  class MessageKafkaDTO
    {
        public MessageKafkaDTO(string name)
        {
            this.Name= name;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
