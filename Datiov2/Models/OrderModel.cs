//using Humanizer;
//CREATE TABLE[dbo].[Orders] (
//    [OrderID]         INT IDENTITY(1, 1) NOT NULL,
//    [OrderUserID]     INT            NOT NULL,
//    [OrderPrice]      INT            NOT NULL,
//    [OrderAddress]    NVARCHAR (100) NOT NULL,

//    [OrderFirstName]  NVARCHAR (100) NOT NULL,

//    [OrderLastName]   NVARCHAR (100) NOT NULL,
//    [OrderPostalCode] INT            NOT NULL,
//    [OrderCity]       NVARCHAR (100) NOT NULL,
//    [OrderDate]       DATETIME       DEFAULT (getdate()) NULL,
//    PRIMARY KEY CLUSTERED ([OrderID] ASC),
//    FOREIGN KEY([OrderUserID]) REFERENCES[dbo].[Users]([UserID])
//);








namespace Datiov2.Models
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public int OrderCartID { get; set; }
        public int OrderUserID { get; set; }
        public int OrderPrice { get; set; }
        public string OrderAddress { get; set; }
        public string OrderFirstName { get; set; }
        public string OrderLastName { get; set; }
        public int OrderPostalCode { get; set; }
        public string OrderCity { get; set; }
        public string OrderDate { get; set; }

    }
}
