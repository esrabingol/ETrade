using ETrade.Abstract;
using System;
using System.Collections.Generic;

namespace ETrade.Entities
{
    public class Order:IEntity
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; } 
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; } 
        public EnumOrderState OrderState { get; set; }
        public EnumPaymentTypes PaymentTypes { get; set; }
        public string  FirstName { get; set; }
        public string  LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
        public string OrderNote { get; set; }
        public int PaymentId { get; set; } //int yapıldı
        public string PaymentToken { get; set; }
        public int ConversationId { get; set; }
        public List<OrderItem> orderItems { get; set; }
    }

    public enum EnumOrderState
       {
        Waiting =0,
        Unpaid=1,
        Completed =2
}
    public enum EnumPaymentTypes
    {
        CreditCart=0,
        Eft =1

    }
}
