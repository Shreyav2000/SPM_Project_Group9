using System;
using System.Collections.Generic;
using HealthCare.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Server;

public partial class HealthcareContext : DbContext
{
    public HealthcareContext()
    {
    }

    public HealthcareContext(DbContextOptions<HealthcareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alldisease> Alldiseases { get; set; }

    public virtual DbSet<Alldiseasecategory> Alldiseasecategories { get; set; }

    public virtual DbSet<Allergy> Allergies { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Billcategory> Billcategories { get; set; }

    public virtual DbSet<Billdetail> Billdetails { get; set; }

    public virtual DbSet<Complaint> Complaints { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<Drug> Drugs { get; set; }

    public virtual DbSet<Drugbill> Drugbills { get; set; }

    public virtual DbSet<Drugbrand> Drugbrands { get; set; }
    public virtual DbSet<DrugStock> DrugStocks{ get; set; }

    //public virtual DbSet<Drugitem> Drugitems { get; set; }

    public virtual DbSet<Expiryitem> Expiryitems { get; set; }

    public virtual DbSet<Investigationcategory> Investigationcategories { get; set; }

    public virtual DbSet<Investigationsession> Investigationsessions { get; set; }

    public virtual DbSet<Itemquantitysummary> Itemquantitysummaries { get; set; }

    public virtual DbSet<Labconsrequest> Labconsrequests { get; set; }

    public virtual DbSet<Labprice> Labprices { get; set; }

    public virtual DbSet<Labresultsdetailssection> Labresultsdetailssections { get; set; }

    public virtual DbSet<Labsession> Labsessions { get; set; }

    public virtual DbSet<Labsessiontest> Labsessiontests { get; set; }

    public virtual DbSet<Labsessiontestshistory> Labsessiontestshistories { get; set; }

    public virtual DbSet<Labtest> Labtests { get; set; }

    public virtual DbSet<Levelofcare> Levelofcares { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Patientattendance> Patientattendances { get; set; }

    public virtual DbSet<Patientattendancebill> Patientattendancebills { get; set; }

    public virtual DbSet<Patientcomplaint> Patientcomplaints { get; set; }

    public virtual DbSet<Patientcomplaintnote> Patientcomplaintnotes { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Prescriptiondetail> Prescriptiondetails { get; set; }

    public virtual DbSet<Remark> Remarks { get; set; }

    public virtual DbSet<Remarkshistory> Remarkshistories { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Transactiontype> Transactiontypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Alldisease>(entity =>
        {
            entity.HasKey(e => e.DiseaseId).HasName("PRIMARY");

            entity.ToTable("alldiseases");

            entity.Property(e => e.DiseaseId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("DiseaseID");
            entity.Property(e => e.CatId)
                .HasColumnType("int(11)")
                .HasColumnName("CatID");
            entity.Property(e => e.DiseaseName).HasMaxLength(200);
            entity.Property(e => e.NoDays).HasColumnType("bigint(20)");
        });

        modelBuilder.Entity<Alldiseasecategory>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("PRIMARY");

            entity.ToTable("alldiseasecategory");

            entity.Property(e => e.CatId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("CatID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Allergy>(entity =>
        {
            entity.HasKey(e => e.AllergyId).HasName("PRIMARY");

            entity.ToTable("allergy");

            entity.Property(e => e.AllergyId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.Allergy1)
                .HasMaxLength(800)
                .HasColumnName("Allergy");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PRIMARY");

            entity.ToTable("bill");

            entity.HasIndex(e => e.Deptid, "FK_Bill_department");

            entity.Property(e => e.BillId)
                .HasMaxLength(15)
                .IsFixedLength();
            entity.Property(e => e.BillDate).HasColumnType("datetime");
            entity.Property(e => e.Deptid)
                .HasColumnType("int(11)")
                .HasColumnName("deptid");
            entity.Property(e => e.PatientId).HasMaxLength(50);
            entity.Property(e => e.StaffId).HasColumnType("int(11)");

            entity.HasOne(d => d.Dept).WithMany(p => p.Bills)
                .HasForeignKey(d => d.Deptid)
                .HasConstraintName("FK_Bill_department");
        });

        modelBuilder.Entity<Billcategory>(entity =>
        {
            entity.HasKey(e => e.BillCategoryId).HasName("PRIMARY");

            entity.ToTable("billcategory");

            entity.Property(e => e.BillCategoryId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("BillCategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Billdetail>(entity =>
        {
            entity.HasKey(e => new { e.BillId, e.ItemId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("billdetail");

            entity.Property(e => e.BillId)
                .HasMaxLength(15)
                .IsFixedLength();
            entity.Property(e => e.ItemId)
                .HasMaxLength(15)
                .IsFixedLength();
            entity.Property(e => e.Quantity).HasColumnType("int(11)");
            entity.Property(e => e.UnitPrice).HasPrecision(19, 4);

            entity.HasOne(d => d.Bill).WithMany(p => p.Billdetails)
                .HasForeignKey(d => d.BillId)
                .HasConstraintName("FK_BillDetail_Bill");
        });

        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.HasKey(e => e.ComplaintId).HasName("PRIMARY");

            entity.ToTable("complaint");

            entity.Property(e => e.ComplaintId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.Complaint1)
                .HasMaxLength(500)
                .HasColumnName("Complaint");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Deptid).HasName("PRIMARY");

            entity.ToTable("department");

            entity.Property(e => e.Deptid)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("deptid");
            entity.Property(e => e.DepType).HasColumnType("int(11)");
            entity.Property(e => e.DeptHead).HasColumnType("int(11)");
            entity.Property(e => e.Deptname)
                .HasMaxLength(50)
                .HasColumnName("deptname");
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.HasKey(e => e.DiagnosisId).HasName("PRIMARY");

            entity.ToTable("diagnosis");

            entity.Property(e => e.DiagnosisId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("diagnosisID");
            entity.Property(e => e.Agecat)
                .HasMaxLength(3)
                .HasColumnName("AGECAT");
            entity.Property(e => e.Catid)
                .HasColumnType("int(11)")
                .HasColumnName("CATID");
            entity.Property(e => e.Diagnosis1)
                .HasMaxLength(100)
                .HasColumnName("diagnosis");
            entity.Property(e => e.Gdrg)
                .HasMaxLength(50)
                .HasColumnName("GDRG");
            entity.Property(e => e.Gendercat)
                .HasMaxLength(2)
                .HasColumnName("GENDERCAT");
            entity.Property(e => e.Oth)
                .HasMaxLength(50)
                .HasColumnName("OTH");
            entity.Property(e => e.Price)
                .HasPrecision(19, 4)
                .HasColumnName("PRICE");
            entity.Property(e => e.Pricead)
                .HasPrecision(19, 4)
                .HasColumnName("PRICEAD");
            entity.Property(e => e.Specid)
                .HasColumnType("int(11)")
                .HasColumnName("SPECID");
        });

        modelBuilder.Entity<Drug>(entity =>
        {
            entity.HasKey(e => e.DrugId).HasName("PRIMARY");

            entity.ToTable("drugs");

            entity.Property(e => e.DrugId)
                .HasMaxLength(50)
                .HasColumnName("DrugID");
            entity.Property(e => e.Drugname)
                .HasMaxLength(255)
                .HasColumnName("drugname");
            entity.Property(e => e.OutOfService);
            entity.Property(e => e.Refill);
            entity.Property(e => e.Price).HasPrecision(18, 2);
        });
        modelBuilder.Entity<DrugStock>(entity =>
        {
            entity.HasKey(e => e.DrugId).HasName("PRIMARY");

            entity.ToTable("drugstock");

            entity.Property(e => e.DrugId)
                .HasMaxLength(50)
                .HasColumnName("drugItem");
            entity.Property(e => e.Quantity)
                .HasColumnName("quantity");
        });
        modelBuilder.Entity<Drugbill>(entity =>
        {
            entity.HasKey(e => new { e.BranchId, e.ReceiptId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("drugbills");

            entity.Property(e => e.BranchId).HasColumnType("int(11)");
            entity.Property(e => e.ReceiptId).HasColumnType("bigint(20)");
            entity.Property(e => e.Amount).HasPrecision(38, 2);
            entity.Property(e => e.AsOfDate).HasColumnType("datetime");
            entity.Property(e => e.BillId).HasColumnType("bigint(20)");
            entity.Property(e => e.ConsultId)
                .HasColumnType("int(11)")
                .HasColumnName("consultID");
            entity.Property(e => e.CustomerId).HasColumnType("int(11)");
            entity.Property(e => e.CustomerName).HasMaxLength(511);
            entity.Property(e => e.CustomerNo).HasMaxLength(50);
            entity.Property(e => e.HbillId)
                .HasMaxLength(100)
                .HasColumnName("HBillID");
            entity.Property(e => e.PatientId).HasColumnType("int(11)");
            entity.Property(e => e.ReceiptNo).HasMaxLength(30);
            entity.Property(e => e.UniqueTransactionId)
                .HasMaxLength(100)
                .HasColumnName("UniqueTransactionID");
        });

        modelBuilder.Entity<Drugbrand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PRIMARY");

            entity.ToTable("drugbrands");

            entity.Property(e => e.BrandId)
                .HasMaxLength(50)
                .HasColumnName("BrandID");
            entity.Property(e => e.BrandName).HasMaxLength(225);
        });

        modelBuilder.Entity<Expiryitem>(entity =>
        {
            entity.HasKey(e => e.Nid).HasName("PRIMARY");

            entity.ToTable("expiryitems");

            entity.Property(e => e.Nid)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ExpDate).HasColumnType("datetime");
            entity.Property(e => e.ItemId)
                .HasMaxLength(150)
                .IsFixedLength()
                .HasColumnName("ItemID");
            entity.Property(e => e.ManDate).HasColumnType("datetime");
            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.Quantity)
                .IsFixedLength();
            entity.Property(e => e.Shelf)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Investigationcategory>(entity =>
        {
            entity.HasKey(e => e.InvestigationCategoryId).HasName("PRIMARY");

            entity.ToTable("investigationcategory");

            entity.Property(e => e.InvestigationCategoryId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.InvestigationCategory1)
                .HasMaxLength(255)
                .HasColumnName("InvestigationCategory");
        });

        modelBuilder.Entity<Investigationsession>(entity =>
        {
            entity.HasKey(e => e.InvestigationSessionId).HasName("PRIMARY");

            entity.ToTable("investigationsession");

            entity.Property(e => e.InvestigationSessionId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.BillAmount).HasPrecision(18, 2);
            entity.Property(e => e.BillId).HasMaxLength(100);
            entity.Property(e => e.Comments).HasMaxLength(0);
            entity.Property(e => e.ConsId)
                .HasColumnType("int(11)")
                .HasColumnName("ConsID");
            entity.Property(e => e.InvestigationCategoryId)
                .HasColumnType("int(11)")
                .HasColumnName("InvestigationCategoryID");
            entity.Property(e => e.IsVoidBy).HasColumnType("int(11)");
            entity.Property(e => e.IsVoidDate).HasColumnType("datetime");
            entity.Property(e => e.PatientId)
                .HasColumnType("int(11)")
                .HasColumnName("PatientID");
            entity.Property(e => e.PatientNo).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus).HasColumnType("int(11)");
            entity.Property(e => e.RequestedBy).HasColumnType("int(11)");
            entity.Property(e => e.SessionDate).HasColumnType("datetime");
            entity.Property(e => e.SessionTime).HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
            entity.Property(e => e.UniqueTransactionId)
                .HasMaxLength(30)
                .HasColumnName("UniqueTransactionID");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("UserID");
        });

        modelBuilder.Entity<Itemquantitysummary>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("PRIMARY");

            entity.ToTable("itemquantitysummary");

            entity.Property(e => e.TransId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("TransID");
            entity.Property(e => e.BranchId)
                .HasColumnType("int(11)")
                .HasColumnName("BranchID");
            entity.Property(e => e.BrandId)
                .HasMaxLength(50)
                .HasColumnName("BrandID");
            entity.Property(e => e.ItemId)
                .HasColumnType("int(11)")
                .HasColumnName("ItemID");
            entity.Property(e => e.Lastupdateddate).HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Labconsrequest>(entity =>
        {
            entity.HasKey(e => e.RecId).HasName("PRIMARY");

            entity.ToTable("labconsrequests");

            entity.Property(e => e.RecId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("RecID");
            entity.Property(e => e.ConsId)
                .HasColumnType("int(11)")
                .HasColumnName("ConsID");
            entity.Property(e => e.InvestigationCategoryId).HasColumnType("int(11)");
            entity.Property(e => e.LabtestId)
                .HasColumnType("int(11)")
                .HasColumnName("LabtestID");
            entity.Property(e => e.PatientId)
                .HasColumnType("int(11)")
                .HasColumnName("PatientID");
            entity.Property(e => e.PatientNo).HasMaxLength(50);
            entity.Property(e => e.RequestSentByDoctorTime).HasColumnType("datetime");
            entity.Property(e => e.TransDate).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("UserID");
        });

        modelBuilder.Entity<Labprice>(entity =>
        {
            entity.HasKey(e => new { e.Latestid, e.Schid, e.LabRequestDestinationId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("labprices");

            entity.Property(e => e.Latestid)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("latestid");
            entity.Property(e => e.Schid)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("schid");
            entity.Property(e => e.LabRequestDestinationId)
                .HasColumnType("int(11)")
                .HasColumnName("LabRequestDestinationID");
            entity.Property(e => e.Priceid)
                .HasPrecision(19, 4)
                .HasColumnName("priceid");
        });

        modelBuilder.Entity<Labresultsdetailssection>(entity =>
        {
            entity.HasKey(e => e.RecNum).HasName("PRIMARY");

            entity.ToTable("labresultsdetailssection");

            entity.Property(e => e.RecNum)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.Comment).HasMaxLength(0);
            entity.Property(e => e.Consid)
                .HasColumnType("int(11)")
                .HasColumnName("CONSID");
            entity.Property(e => e.Flvid)
                .HasColumnType("int(11)")
                .HasColumnName("FLVID");
            entity.Property(e => e.LabSessionId).HasColumnType("int(11)");
            entity.Property(e => e.LabTestId)
                .HasColumnType("int(11)")
                .HasColumnName("LabTestID");
            entity.Property(e => e.LocalId)
                .HasColumnType("int(11)")
                .HasColumnName("LocalID");
            entity.Property(e => e.Slid)
                .HasColumnType("int(11)")
                .HasColumnName("SLID");
            entity.Property(e => e.TestRange).HasMaxLength(250);
            entity.Property(e => e.Tlid)
                .HasColumnType("int(11)")
                .HasColumnName("TLID");
        });

        modelBuilder.Entity<Labsession>(entity =>
        {
            entity.HasKey(e => e.LabSessionId).HasName("PRIMARY");

            entity.ToTable("labsession");

            entity.Property(e => e.LabSessionId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.BillId).HasMaxLength(100);
            entity.Property(e => e.BilledAmount).HasPrecision(18, 2);
            entity.Property(e => e.CategoryId)
                .HasColumnType("int(11)")
                .HasColumnName("CategoryID");
            entity.Property(e => e.Checked)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("checked");
            entity.Property(e => e.Comments).HasMaxLength(0);
            entity.Property(e => e.Consid)
                .HasColumnType("int(11)")
                .HasColumnName("CONSID");
            entity.Property(e => e.ContactAddress).HasMaxLength(0);
            entity.Property(e => e.DateReported).HasColumnType("datetime");
            entity.Property(e => e.Doctor).HasMaxLength(200);
            entity.Property(e => e.Finished)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.LabBarcodeStandardNo).HasMaxLength(13);
            entity.Property(e => e.LabNo)
                .HasColumnType("int(11)")
                .HasColumnName("labNo");
            entity.Property(e => e.LabSessionDate).HasColumnType("datetime");
            entity.Property(e => e.Nhilno)
                .HasMaxLength(50)
                .HasColumnName("NHILNO");
            entity.Property(e => e.PatientId).HasColumnType("int(11)");
            entity.Property(e => e.PatientNo).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus).HasColumnType("int(11)");
            entity.Property(e => e.RequestingFacilityId)
                .HasColumnType("int(11)")
                .HasColumnName("RequestingFacilityID");
            entity.Property(e => e.SampDate).HasColumnType("datetime");
            entity.Property(e => e.SchPro)
                .HasColumnType("int(11)")
                .HasColumnName("schPro");
            entity.Property(e => e.Schemeid)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("schemeid");
            entity.Property(e => e.ServicePointDescriptionId)
                .HasColumnType("int(11)")
                .HasColumnName("ServicePointDescriptionID");
            entity.Property(e => e.StaffId)
                .HasColumnType("int(11)")
                .HasColumnName("StaffID");
            entity.Property(e => e.StandardLabNo).HasMaxLength(13);
            entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
            entity.Property(e => e.UniqueTransactionId)
                .HasMaxLength(30)
                .HasColumnName("UniqueTransactionID");
            entity.Property(e => e.VerCol)
                .HasMaxLength(8)
                .IsFixedLength();
        });

        modelBuilder.Entity<Labsessiontest>(entity =>
        {
            entity.HasKey(e => new { e.LabSessionId, e.LabTestId, e.LocalId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("labsessiontests");

            entity.Property(e => e.LabSessionId).HasColumnType("int(11)");
            entity.Property(e => e.LabTestId)
                .HasColumnType("int(11)")
                .HasColumnName("LabTestID");
            entity.Property(e => e.LocalId)
                .HasColumnType("int(11)")
                .HasColumnName("LocalID");
            entity.Property(e => e.Consid)
                .HasColumnType("int(11)")
                .HasColumnName("CONSID");
            entity.Property(e => e.Ddate)
                .HasColumnType("datetime")
                .HasColumnName("DDATE");
            entity.Property(e => e.Finished)
                .HasMaxLength(2)
                .IsFixedLength();
            entity.Property(e => e.Flag).HasMaxLength(20);
            entity.Property(e => e.IsCash).HasDefaultValueSql("'0'");
            entity.Property(e => e.LabRequestDestinationId)
                .HasColumnType("int(11)")
                .HasColumnName("LabRequestDestinationID");
            entity.Property(e => e.Nno)
                .HasMaxLength(50)
                .HasColumnName("NNO");
            entity.Property(e => e.RPrice)
                .HasPrecision(19, 4)
                .HasColumnName("rPrice");
            entity.Property(e => e.RangeId)
                .HasColumnType("int(11)")
                .HasColumnName("RangeID");
            entity.Property(e => e.Recno)
                .HasColumnType("int(11)")
                .HasColumnName("RECNO");
            entity.Property(e => e.Result).HasMaxLength(0);
            entity.Property(e => e.ResultComment).HasMaxLength(400);
            entity.Property(e => e.ResultPercentageValues).HasMaxLength(0);
            entity.Property(e => e.ResultRange).HasMaxLength(250);
            entity.Property(e => e.ResultSentTime).HasColumnType("datetime");
            entity.Property(e => e.ResultUnit).HasMaxLength(50);
            entity.Property(e => e.ResultsSignedBy).HasColumnType("int(11)");
            entity.Property(e => e.SchemeOptionTypeId)
                .HasColumnType("int(11)")
                .HasColumnName("SchemeOptionTypeID");
            entity.Property(e => e.Schid)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SCHID");
            entity.Property(e => e.Schpro)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SCHPRO");
            entity.Property(e => e.Sellt).HasColumnName("SELLT");
            entity.Property(e => e.VerCol)
                .HasMaxLength(8)
                .IsFixedLength();
        });

        modelBuilder.Entity<Labsessiontestshistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("labsessiontestshistory");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.Consid)
                .HasColumnType("int(11)")
                .HasColumnName("CONSID");
            entity.Property(e => e.Ddate)
                .HasColumnType("datetime")
                .HasColumnName("DDATE");
            entity.Property(e => e.Finished)
                .HasMaxLength(2)
                .IsFixedLength();
            entity.Property(e => e.Flag).HasMaxLength(20);
            entity.Property(e => e.LabRequestDestinationId)
                .HasColumnType("int(11)")
                .HasColumnName("LabRequestDestinationID");
            entity.Property(e => e.LabSessionId).HasColumnType("int(11)");
            entity.Property(e => e.LabTestId)
                .HasColumnType("int(11)")
                .HasColumnName("LabTestID");
            entity.Property(e => e.LocalId)
                .HasColumnType("int(11)")
                .HasColumnName("LocalID");
            entity.Property(e => e.Nno)
                .HasMaxLength(50)
                .HasColumnName("NNO");
            entity.Property(e => e.RPrice)
                .HasPrecision(19, 4)
                .HasColumnName("rPrice");
            entity.Property(e => e.RangeId)
                .HasColumnType("int(11)")
                .HasColumnName("RangeID");
            entity.Property(e => e.Recno)
                .HasColumnType("int(11)")
                .HasColumnName("RECNO");
            entity.Property(e => e.Result).HasMaxLength(0);
            entity.Property(e => e.ResultComment).HasMaxLength(400);
            entity.Property(e => e.ResultPercentageValues).HasMaxLength(0);
            entity.Property(e => e.ResultRange).HasMaxLength(250);
            entity.Property(e => e.ResultSentTime).HasColumnType("datetime");
            entity.Property(e => e.ResultUnit).HasMaxLength(50);
            entity.Property(e => e.ResultsSignedBy).HasColumnType("int(11)");
            entity.Property(e => e.SchemeOptionTypeId)
                .HasColumnType("int(11)")
                .HasColumnName("SchemeOptionTypeID");
            entity.Property(e => e.Schid)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SCHID");
            entity.Property(e => e.Schpro)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SCHPRO");
            entity.Property(e => e.Sellt).HasColumnName("SELLT");
            entity.Property(e => e.UpdatedBy).HasColumnType("int(11)");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.VerCol)
                .HasMaxLength(8)
                .IsFixedLength();
        });

        modelBuilder.Entity<Labtest>(entity =>
        {
            entity.HasKey(e => e.LabTestId).HasName("PRIMARY");

            entity.ToTable("labtest");

            entity.HasIndex(e => e.InvestigationCategoryId, "FK_Labtest_InvestigationCategory");

            entity.Property(e => e.LabTestId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("LabTestID");
            entity.Property(e => e.Agecat)
                .HasMaxLength(3)
                .HasColumnName("AGECAT");
            entity.Property(e => e.AmtPrice).HasPrecision(19, 4);
            entity.Property(e => e.Category)
                .HasColumnType("int(11)")
                .HasColumnName("CATEGORY");
            entity.Property(e => e.ExamId)
                .HasColumnType("int(11)")
                .HasColumnName("ExamID");
            entity.Property(e => e.Gdrg)
                .HasMaxLength(50)
                .HasColumnName("GDRG");
            entity.Property(e => e.Gendercat)
                .HasMaxLength(2)
                .HasColumnName("GENDERCAT");
            entity.Property(e => e.Icd)
                .HasMaxLength(50)
                .HasColumnName("ICD");
            entity.Property(e => e.InvestigationCategoryId).HasColumnType("int(11)");
            entity.Property(e => e.LabTest1)
                .HasMaxLength(120)
                .HasColumnName("LabTest");
            entity.Property(e => e.Pricead)
                .HasPrecision(19, 4)
                .HasColumnName("PRICEAD");
            entity.Property(e => e.ReportId)
                .HasColumnType("int(11)")
                .HasColumnName("ReportID");
            entity.Property(e => e.SortOrder).HasColumnType("int(11)");
            entity.Property(e => e.Specid)
                .HasColumnType("int(11)")
                .HasColumnName("SPECID");
            entity.Property(e => e.SpecimenId)
                .HasColumnType("int(11)")
                .HasColumnName("SpecimenID");
            entity.Property(e => e.TransDate).HasColumnType("datetime");
            entity.Property(e => e.Units).HasMaxLength(20);
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("UserID");
            entity.Property(e => e.VRange)
                .HasMaxLength(50)
                .HasColumnName("vRange");

            entity.HasOne(d => d.InvestigationCategory).WithMany(p => p.Labtests)
                .HasForeignKey(d => d.InvestigationCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Labtest_InvestigationCategory");
        });

        modelBuilder.Entity<Levelofcare>(entity =>
        {
            entity.HasKey(e => e.LevelId).HasName("PRIMARY");

            entity.ToTable("levelofcare");

            entity.Property(e => e.LevelId)
                .HasMaxLength(2)
                .HasColumnName("LevelID");
            entity.Property(e => e.LevelOfCare1)
                .HasMaxLength(50)
                .HasColumnName("LevelOfCare");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PRIMARY");

            entity.ToTable("patient");

            entity.Property(e => e.PatientId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.AlternateId).HasMaxLength(50);
            entity.Property(e => e.BloodGroup).HasMaxLength(50);
            entity.Property(e => e.CardSerial).HasMaxLength(200);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.Consultingroom)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("consultingroom");
            entity.Property(e => e.Country).HasColumnType("smallint(6)");
            entity.Property(e => e.DateRegistered).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Fname).HasMaxLength(50);
            entity.Property(e => e.LegacyPatientId).HasMaxLength(50);
            entity.Property(e => e.LegacyPatientNo).HasMaxLength(100);
            entity.Property(e => e.Lname).HasMaxLength(50);
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Nationality).HasMaxLength(50);
            entity.Property(e => e.Nhis)
                .HasMaxLength(50)
                .HasColumnName("NHIS");
            entity.Property(e => e.NoKdetails)
                .HasMaxLength(200)
                .HasColumnName("NoKDetails");
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.PatientNo).HasMaxLength(50);
            entity.Property(e => e.Picture).HasColumnName("picture");
            entity.Property(e => e.PostalAddress).HasMaxLength(255);
            entity.Property(e => e.Provid)
                .HasColumnType("int(11)")
                .HasColumnName("PROVID");
            entity.Property(e => e.Province).HasMaxLength(50);
            entity.Property(e => e.Region)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("region");
            entity.Property(e => e.Sex)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("sex");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .HasColumnName("street");
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .HasColumnName("telephone");
            entity.Property(e => e.Telephone2)
                .HasMaxLength(50)
                .HasColumnName("telephone2");
            entity.Property(e => e.Title)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Patientattendance>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("patientattendance");

            entity.Property(e => e.AgePrefix).HasMaxLength(5);
            entity.Property(e => e.BillState)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ConsultId)
                .HasColumnType("int(11)")
                .HasColumnName("consultID");
            entity.Property(e => e.DepartmentCode).HasMaxLength(10);
            entity.Property(e => e.FinalDischargeDate).HasColumnType("datetime");
            entity.Property(e => e.PTime)
                .HasColumnType("datetime")
                .HasColumnName("pTime");
            entity.Property(e => e.PatientCategory).HasMaxLength(50);
            entity.Property(e => e.PatientCategorySubTypeId)
                .HasColumnType("int(11)")
                .HasColumnName("PatientCategorySubTypeID");
            entity.Property(e => e.PatientDepartmentId)
                .HasColumnType("int(11)")
                .HasColumnName("PatientDepartmentID");
            entity.Property(e => e.PatientId).HasColumnType("int(11)");
            entity.Property(e => e.PatientNo).HasMaxLength(50);
            entity.Property(e => e.Provider).HasColumnType("int(11)");
            entity.Property(e => e.Reason)
                .HasMaxLength(50)
                .HasColumnName("reason");
            entity.Property(e => e.SeenByDoctorId)
                .HasColumnType("int(11)")
                .HasColumnName("SeenByDoctorID");
            entity.Property(e => e.SeenByDoctorTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<Patientattendancebill>(entity =>
        {
            entity.HasKey(e => new { e.BranchId, e.BillId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("patientattendancebill");

            entity.Property(e => e.BranchId).HasColumnType("int(11)");
            entity.Property(e => e.BillId).HasColumnType("bigint(20)");
            entity.Property(e => e.ConsultId).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Patientcomplaint>(entity =>
        {
            entity.HasKey(e => new { e.ConsultId, e.ComplaintId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("patientcomplaint");

            entity.Property(e => e.ConsultId)
                .HasColumnType("int(11)")
                .HasColumnName("consultID");
            entity.Property(e => e.ComplaintId).HasColumnType("int(11)");
            entity.Property(e => e.PatientComplaintDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp");
            entity.Property(e => e.PatientId).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Patientcomplaintnote>(entity =>
        {
            entity.HasKey(e => new { e.ConsultId, e.UserId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("patientcomplaintnotes");

            entity.Property(e => e.ConsultId)
                .HasColumnType("int(11)")
                .HasColumnName("consultID");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("UserID");
            entity.Property(e => e.Notes).HasMaxLength(8000);
            entity.Property(e => e.PatientComplaintNotesDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp");
            entity.Property(e => e.PatientId).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("prescription");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.ConsId)
                .HasColumnType("int(11)")
                .HasColumnName("ConsID");
            entity.Property(e => e.DoctorDetails).HasMaxLength(90);
            entity.Property(e => e.DoctorRequesting).HasColumnType("int(11)");
            entity.Property(e => e.NotifyViaEmailaheadDays).HasColumnType("int(11)");
            entity.Property(e => e.NotifyViaSmsaheadDays)
                .HasColumnType("int(11)")
                .HasColumnName("NotifyViaSMSaheadDays");
            entity.Property(e => e.PatientId)
                .HasColumnType("int(11)")
                .HasColumnName("PatientID");
            entity.Property(e => e.PatientNo).HasMaxLength(50);
            entity.Property(e => e.PrescriptionSessionId)
                .HasMaxLength(50)
                .HasColumnName("PrescriptionSessionID");
            entity.Property(e => e.SendSms).HasColumnName("SendSMS");
            entity.Property(e => e.TransDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Prescriptiondetail>(entity =>
        {
            entity.HasKey(e => e.RecNo).HasName("PRIMARY");

            entity.ToTable("prescriptiondetails");

            entity.Property(e => e.RecNo)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.ConsId)
                .HasColumnType("int(11)")
                .HasColumnName("ConsID");
            entity.Property(e => e.Ddate).HasColumnType("datetime");
            entity.Property(e => e.DispTotal).HasPrecision(18, 2);
            entity.Property(e => e.Dosage).HasPrecision(18, 2);
            entity.Property(e => e.DosageUnit).HasMaxLength(20);
            entity.Property(e => e.DrugId)
                .HasColumnName("DrugID");
            entity.Property(e => e.EditedBy).HasColumnType("int(11)");
            entity.Property(e => e.EditedDate).HasColumnType("datetime");
            entity.Property(e => e.FormulationDesc).HasColumnType("int(11)");
            entity.Property(e => e.Frequency).HasMaxLength(50);
            entity.Property(e => e.FrequencyQty).HasColumnType("int(11)");
            entity.Property(e => e.NoofDays).HasColumnType("int(11)");
            entity.Property(e => e.Notes).HasMaxLength(0);
            entity.Property(e => e.PrescriptionSessionId)
                .HasMaxLength(50)
                .HasColumnName("PrescriptionSessionID");
            entity.Property(e => e.ProviderId)
                .HasColumnType("int(11)")
                .HasColumnName("ProviderID");
            entity.Property(e => e.Route).HasMaxLength(50);
            entity.Property(e => e.ServiceNumber).HasMaxLength(50);
            entity.Property(e => e.ServiceOptionId)
                .HasMaxLength(10)
                .HasColumnName("ServiceOptionID");
            entity.Property(e => e.Total).HasPrecision(18, 2);
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("UserID");
            entity.Property(e => e.Valid).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Remark>(entity =>
        {
            entity.HasKey(e => new { e.PatientId, e.ConsId, e.UserId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("remarks");

            entity.Property(e => e.PatientId)
                .HasColumnType("int(11)")
                .HasColumnName("PatientID");
            entity.Property(e => e.ConsId)
                .HasColumnType("int(11)")
                .HasColumnName("ConsID");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("UserID");
            entity.Property(e => e.AdmittedOrReferredDate).HasColumnType("datetime");
            entity.Property(e => e.Comments).HasMaxLength(0);
            entity.Property(e => e.CommentsTwo).HasMaxLength(0);
            entity.Property(e => e.PatientNo).HasMaxLength(50);
            entity.Property(e => e.Remark1)
                .HasMaxLength(50)
                .HasColumnName("Remark");
            entity.Property(e => e.SentTo).HasColumnType("int(11)");
            entity.Property(e => e.TransDate).HasColumnType("datetime");
            entity.Property(e => e.TransTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<Remarkshistory>(entity =>
        {
            entity.HasKey(e => new { e.PatientId, e.ConsId, e.UserId, e.Id })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

            entity.ToTable("remarkshistory");

            entity.Property(e => e.PatientId)
                .HasColumnType("int(11)")
                .HasColumnName("PatientID");
            entity.Property(e => e.ConsId)
                .HasColumnType("int(11)")
                .HasColumnName("ConsID");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("UserID");
            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.AdmittedOrReferredDate).HasColumnType("datetime");
            entity.Property(e => e.Comments).HasMaxLength(0);
            entity.Property(e => e.CommentsTwo).HasMaxLength(0);
            entity.Property(e => e.PatientNo).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.SentTo).HasColumnType("int(11)");
            entity.Property(e => e.TransDate).HasColumnType("datetime");
            entity.Property(e => e.TransTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Staffid).HasName("PRIMARY");

            entity.ToTable("staff");

            entity.Property(e => e.Staffid)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("staffid");
            entity.Property(e => e.BranchId)
                .HasColumnType("int(11)")
                .HasColumnName("BranchID");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.DateRegistered).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Fname).HasMaxLength(50);
            entity.Property(e => e.Lname).HasMaxLength(50);
            entity.Property(e => e.Picture).HasColumnName("picture");
            entity.Property(e => e.Province).HasMaxLength(50);
            entity.Property(e => e.Sex)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("sex");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .HasColumnName("street");
            entity.Property(e => e.Telephone1)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("telephone1");
            entity.Property(e => e.Telephone2)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("telephone2");
        });

        modelBuilder.Entity<Transactiontype>(entity =>
        {
            entity.HasKey(e => e.TransactionTypeId).HasName("PRIMARY");

            entity.ToTable("transactiontype");

            entity.Property(e => e.TransactionTypeId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("TransactionTypeID");
            entity.Property(e => e.TransactionTypeName).HasMaxLength(50);
        });
        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PRIMARY");

            entity.ToTable("permissions");

            entity.Property(e => e.PermissionId)
                .HasColumnType("int(11)")
                .HasColumnName("permission_id");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(50)
                .HasColumnName("permission_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.RoleId, "role_id");

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_ibfk_1");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("user_roles");

            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("permission_id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("role_permissions_ibfk_2"),
                    l => l.HasOne<UserRole>().WithMany()
                        .HasForeignKey("role_id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("role_permissions_ibfk_1"),
                    j =>
                    {
                        j.HasKey("role_id", "permission_id")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("role_permissions");
                        j.HasIndex(new[] { "permission_id" }, "permission_id");
                    });
        });
        modelBuilder.Entity<Ward>(entity =>
        {
            entity.HasKey(e => e.WardId).HasName("PRIMARY");

            entity.ToTable("ward");

            entity.Property(e => e.WardId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.CRoomId)
                .HasColumnType("int(11)")
                .HasColumnName("cRoomId");
            entity.Property(e => e.WardName).HasMaxLength(50);
            entity.Property(e => e.WardTypeId)
                .HasColumnType("int(11)")
                .HasColumnName("WardTypeID");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
