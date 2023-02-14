using System;
using KNUST_Medical_Refund.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace KNUST_Medical_Refund.Server
{
    public partial class KNUSTMISContext : DbContext
    {
        public KNUSTMISContext(DbContextOptions<KNUSTMISContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AiprocessedDocument> AiprocessedDocuments { get; set; }
        public virtual DbSet<ClaimantInfo> ClaimantInfos { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<PreprocessedDate> PreprocessedDates { get; set; }
        public virtual DbSet<RefundRequestInfo> RefundRequestInfos { get; set; }
        public virtual DbSet<RefundSupportDocument> RefundSupportDocuments { get; set; }
        public virtual DbSet<RefundUser> RefundUsers { get; set; }
        public virtual DbSet<RequestBeneficiary> RequestBeneficiaries { get; set; }
        public virtual DbSet<TblAirawdatum> TblAirawdata { get; set; }
        public virtual DbSet<TblDateAttended> TblDateAttendeds { get; set; }
        public virtual DbSet<TblProtest> TblProtests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VettedDocument> VettedDocuments { get; set; }
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        public virtual DbSet<PrescriptionDetail> PrescriptionDetails { get; set; }
        public virtual DbSet<PreTranscribe> PreTranscribes { get; set; }
        public virtual DbSet<PharmacyParse> PharmacyParses { get; set; }
        public virtual DbSet<DrugPrescriptionFrequency> DrugPrescriptionFrequencies { get; set; }
        public virtual DbSet<DrugRouteOfAdministration> DrugRouteOfAdministrations { get; set; }
        public virtual DbSet<DrugUsageForm> DrugUsageForms { get; set; }
        public virtual DbSet<PharmacyTranscribe> PharmacyTranscribes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<RefundSupportDocumentCheck> RefundSupportDocumentChecks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.Entity<AiprocessedDocument>(entity =>
            {
                entity.HasKey(e => e.ParseId)
                    .HasName("PK_tblProcessedDocument");

                entity.ToTable("AIProcessedDocument");

                entity.Property(e => e.ParseId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("parseID");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.AmountState)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("amountState");

                entity.Property(e => e.Client).HasColumnName("client");

                entity.Property(e => e.ClientState)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clientState");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.DateState)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dateState");

                entity.Property(e => e.DocIdstate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("docIDState");

                entity.Property(e => e.DocumentId).HasColumnName("documentID");

                entity.Property(e => e.Duplicate).HasColumnName("duplicate");

                entity.Property(e => e.ExistingId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("existingID");

                entity.Property(e => e.Institution).HasColumnName("institution");

                entity.Property(e => e.InstitutionState)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("institutionState");

                entity.Property(e => e.ReportId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("reportID");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.TypeState)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("typeState");
            });
            modelBuilder.Entity<ClaimantInfo>(entity =>
            {
                entity.HasKey(e => e.ClaimantId);

                entity.ToTable("ClaimantInfo");

                entity.Property(e => e.ClaimantId)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("claimantID");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Department)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("department");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fullname");

                entity.Property(e => e.HospitalNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("hospital_no");

                entity.Property(e => e.Knustid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("knustid");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("telephone");
            });
            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.HasKey(e => e.TypeCode);

                entity.Property(e => e.TypeCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("typeCode");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("typeName");
            });
            modelBuilder.Entity<PreprocessedDate>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("PreprocessedDate");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("requestID");

                entity.Property(e => e.DatePreprocessed).HasColumnName("datePreprocessed");
            });
            modelBuilder.Entity<RefundRequestInfo>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK_tblRefundRequest");

                entity.ToTable("RefundRequestInfo");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("requestID");

                entity.Property(e => e.ApprovedAmount)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("approvedAmount");

                entity.Property(e => e.BeneficiaryType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("beneficiaryType");
               
                entity.Property(e => e.BeneficiaryCategory)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("beneficiaryCat");

                entity.Property(e => e.ClaimantId)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("claimantID");

                entity.Property(e => e.HospitalAttended)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("hospital_attended");

                entity.Property(e => e.ProcessingDate).HasColumnName("processingDate");

                entity.Property(e => e.Reason)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("reason");

                entity.Property(e => e.RequestDate).HasColumnName("requestDate");

                entity.Property(e => e.RequestedAmount)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("requestedAmount");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.VetterId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("vetterID");
            });
            modelBuilder.Entity<RefundSupportDocument>(entity =>
            {
                entity.HasKey(e => e.DocumentGuid)
                    .HasName("PK_tblRefundSupportDocument");

                entity.Property(e => e.DocumentGuid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("documentGUID");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.Attachment)
                    .HasMaxLength(54)
                    .IsUnicode(false)
                    .HasColumnName("Attachment");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(90)
                    .IsUnicode(false)
                    .HasColumnName("clientName");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CompanyName");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.DocumentId)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("documentID");

                entity.Property(e => e.DocumentType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("documentType");

                entity.Property(e => e.Item)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("item");

                entity.Property(e => e.ParseId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("parseID");

                entity.Property(e => e.Prescriber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prescriber");

                entity.Property(e => e.ReportId)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("reportID");

                entity.Property(e => e.Requester)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("requester");
                entity.Property(e => e.external).HasColumnName("external");
                entity.Property(e => e.VetState).HasColumnName("vetState");
            });
            modelBuilder.Entity<RefundUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_tblRefundUsers");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("userID");

                entity.Property(e => e.Group)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("group");

                entity.Property(e => e.Role)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("role");
                entity.Property(e => e.Transcriber)
                   .HasColumnName("transcriber");
                entity.Property(e => e.Requester)
                   .HasColumnName("requester");
            });
            modelBuilder.Entity<RequestBeneficiary>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK_RequestDetails");

                entity.ToTable("RequestBeneficiary");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("requestID");

                entity.Property(e => e.BeneficaryId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("beneficaryID");

                entity.Property(e => e.BeneficiaryAge).HasColumnName("beneficiaryAge");

                entity.Property(e => e.BeneficiaryFullname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("beneficiaryFullname");

                entity.Property(e => e.HospitalNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("hospital_no");

                entity.Property(e => e.Nhisnumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nhisnumber");
            });
            modelBuilder.Entity<TblAirawdatum>(entity =>
            {
                entity.HasKey(e => e.ParseId);

                entity.ToTable("tblAIRawdata");

                entity.Property(e => e.ParseId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("parseID");

                entity.Property(e => e.Data)
                    .IsUnicode(false)
                    .HasColumnName("data");
            });
            modelBuilder.Entity<TblDateAttended>(entity =>
            {
                entity.HasKey(e => e.CountId);

                entity.ToTable("tblDateAttended");

                entity.Property(e => e.CountId).HasColumnName("countID");

                entity.Property(e => e.DateAttended).HasColumnName("dateAttended");

                entity.Property(e => e.ReportId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("reportID");
            });
            modelBuilder.Entity<TblProtest>(entity =>
            {
                entity.HasKey(e => e.ProtestId);

                entity.ToTable("tblProtests");

                entity.Property(e => e.ProtestId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("protestID");

                entity.Property(e => e.AmountApproved)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("amountApproved");

                entity.Property(e => e.Comments)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("comments");

                entity.Property(e => e.DateProcessed).HasColumnName("dateprocessed");

                entity.Property(e => e.DateProtested).HasColumnName("dateprotested");

                entity.Property(e => e.Open).HasColumnName("open");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("requestID");
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User_");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Created")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_Modified")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.HashedPassword)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(256);

                entity.Property(e => e.PasswordHint)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<VettedDocument>(entity =>
            {
                entity.HasKey(e => e.DocumentGuid)
                    .HasName("PK_tblProcessedRefunds");

                entity.Property(e => e.DocumentGuid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("documentGUID");

                entity.Property(e => e.AmountApproved)
                    .HasColumnType("decimal(19, 2)")
                    .HasColumnName("amountApproved");

                entity.Property(e => e.Comments)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("comments");

                entity.Property(e => e.ComparableMarket).HasColumnName("comparableMarket");

                entity.Property(e => e.DocumentDate).HasColumnName("documentDate");

                entity.Property(e => e.DocumentType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("documentType");

                entity.Property(e => e.NhisCovered).HasColumnName("nhisCovered");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("requestID");

                entity.Property(e => e.Satisfy).HasColumnName("satisfy");

                entity.Property(e => e.VetterId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("vetterID");
            });
            modelBuilder.Entity<PharmacyParse>(entity =>
            {
                entity.ToTable("PharmacyParse");
                entity.HasKey(e => e.TransId);

                entity.Property(e => e.RequestId)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("requestID");
                entity.Property(e => e.TransAuthor)
                   .HasMaxLength(30)
                   .IsUnicode(false)
                   .HasColumnName("transAuthor");

                entity.Property(e => e.TransDate)
                    .IsUnicode(false)
                    .HasColumnName("transDate");
            });
            modelBuilder.Entity<DrugPrescriptionFrequency>(entity =>
            {
                entity.ToTable("DrugPrescriptionFrequency");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FrequencyName).HasMaxLength(50);
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("category");

                entity.Property(e => e.Desciption)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ID");

                entity.Property(e => e.Prefix)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StaffCategory)
                    .IsRequired()
                    .HasMaxLength(50);
            });
            modelBuilder.Entity<PharmacyTranscribe>(entity =>
            {
                entity.HasKey(e => e.PharmId);

                entity.ToTable("PharmacyTranscribe");

                entity.Property(e => e.PharmId)
                    .HasColumnName("pharmID");

                entity.Property(e => e.Dosage).HasColumnName("dosage");

                entity.Property(e => e.Drugname)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("drugname");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.Frequency).HasColumnName("frequency");

                entity.Property(e => e.Route).HasColumnName("route");
                entity.Property(e => e.Notes)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("notes");
                entity.Property(e => e.TransId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("transID");
                entity.Property(e => e.DocumentGUID)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("documentGUID");
                entity.Property(e => e.Unit)
                    .HasColumnName("unit");
                entity.Property(e => e.DatePurchased).HasColumnName("datepurchased");
            });
            modelBuilder.Entity<DrugRouteOfAdministration>(entity =>
            {
                entity.ToTable("DrugRouteOfAdministration");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(10);
            });
            modelBuilder.Entity<DrugUsageForm>(entity =>
            {
                entity.HasKey(e => e.UsageFormId);

                entity.ToTable("DrugUsageForm");

                entity.Property(e => e.UsageFormId).HasColumnName("UsageFormID");

                entity.Property(e => e.FormName).HasMaxLength(50);
            });
            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.ToTable("Prescription");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ConsId).HasColumnName("ConsID");

                entity.Property(e => e.DoctorDetails).HasMaxLength(90);

                entity.Property(e => e.DrugsRefillDate).HasColumnType("date");

                entity.Property(e => e.NotifyViaSmsaheadDays).HasColumnName("NotifyViaSMSaheadDays");

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.Property(e => e.PatientNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrescriptionSessionId)
                    .HasMaxLength(50)
                    .HasColumnName("PrescriptionSessionID");

                entity.Property(e => e.SendSms).HasColumnName("SendSMS");

                entity.Property(e => e.TransDate).HasColumnType("datetime");
            });
            modelBuilder.Entity<PrescriptionDetail>(entity =>
            {
                entity.HasKey(e => e.RecNo)
                    .HasName("PK_PrescriptionDetails_1");

                entity.Property(e => e.ConsId).HasColumnName("ConsID");

                entity.Property(e => e.Ddate).HasColumnType("datetime");

                entity.Property(e => e.DispTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Dosage).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DosageUnit).HasMaxLength(20);

                entity.Property(e => e.DrugId).HasColumnName("DrugID");

                entity.Property(e => e.EditedDate).HasColumnType("datetime");

                entity.Property(e => e.Frequency).HasMaxLength(50);

                entity.Property(e => e.PrescriptionSessionId)
                    .HasMaxLength(50)
                    .HasColumnName("PrescriptionSessionID");

                entity.Property(e => e.ProviderId).HasColumnName("ProviderID");

                entity.Property(e => e.Route).HasMaxLength(50);

                entity.Property(e => e.ServiceNumber).HasMaxLength(50);

                entity.Property(e => e.ServiceOptionId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ServiceOptionID");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });
            modelBuilder.Entity<PreTranscribe>(entity =>
            {
                entity.HasKey(e => e.PharmId);

                entity.ToTable("PreTranscribe");

                entity.Property(e => e.PharmId)
                .HasMaxLength(50)
                    .IsUnicode(false)
                .HasColumnName("pharmID");

                entity.Property(e => e.DocumentGuid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("documentGUID");

                entity.Property(e => e.Dosage).HasColumnName("dosage");

                entity.Property(e => e.Drugname)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("drugname");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.Frequency).HasColumnName("frequency");

                entity.Property(e => e.Notes)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("notes");

                entity.Property(e => e.Purchased).HasColumnName("purchased");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("requestID");

                entity.Property(e => e.Route).HasColumnName("route");

                entity.Property(e => e.TransId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("transID");

                entity.Property(e => e.Unit).HasColumnName("unit");
                entity.Property(e => e.DatePurchased).HasColumnName("datepurchased");
            });
            modelBuilder.Entity<RefundSupportDocumentCheck>(entity =>
            {
                entity.ToTable("RefundSupportDocumentChecks");
                entity.Property(e => e.Id)
                     .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.DocumentGuid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("documentGUID");

                entity.Property(e => e.Notes)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("notes");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("requestID");

                entity.Property(e => e.Timestamp).HasColumnName("timestamp");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
