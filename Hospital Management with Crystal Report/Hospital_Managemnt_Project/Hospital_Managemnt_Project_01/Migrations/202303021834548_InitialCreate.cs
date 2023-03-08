namespace Hospital_Managemnt_Project_01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BillDetails",
                c => new
                    {
                        BillDetailId = c.Int(nullable: false, identity: true),
                        Advance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HealthCard = c.Boolean(nullable: false),
                        BillId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillDetailId)
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
                .Index(t => t.BillId);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillId = c.Int(nullable: false, identity: true),
                        BillDate = c.DateTime(nullable: false),
                        SeatRent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherCharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillId)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientId = c.Int(nullable: false, identity: true),
                        PatientName = c.String(nullable: false, maxLength: 80),
                        Phone = c.Int(nullable: false),
                        Gender = c.String(nullable: false, maxLength: 100),
                        Address = c.String(nullable: false, maxLength: 100),
                        DoctorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PatientId)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        DoctorName = c.String(nullable: false, maxLength: 100),
                        BirthDate = c.DateTime(nullable: false),
                        DoctorFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsAvaliable = c.Boolean(nullable: false),
                        Picture = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.DoctorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BillDetails", "BillId", "dbo.Bills");
            DropForeignKey("dbo.Bills", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Patients", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.Patients", new[] { "DoctorId" });
            DropIndex("dbo.Bills", new[] { "PatientId" });
            DropIndex("dbo.BillDetails", new[] { "BillId" });
            DropTable("dbo.Doctors");
            DropTable("dbo.Patients");
            DropTable("dbo.Bills");
            DropTable("dbo.BillDetails");
        }
    }
}
