using System;
using Model.Enum;

namespace Model
{
    public class TicketModel
    {
        public int? ExpenseId { get; set; }
        public int TicketId { get; set; }
        public string TicketName { get; set; }
        public TicketType TicketType { get; set; }
        public decimal TicketMoney{get;set;}
        public DateTime? TicketDate { get; set; }
        public TicketState State { get; set; }
    }
}
