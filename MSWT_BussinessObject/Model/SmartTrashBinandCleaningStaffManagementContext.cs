using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MSWT_BussinessObject.Model;

public partial class SmartTrashBinandCleaningStaffManagementContext : DbContext
{
    public SmartTrashBinandCleaningStaffManagementContext()
    {
    }

    public SmartTrashBinandCleaningStaffManagementContext(DbContextOptions<SmartTrashBinandCleaningStaffManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alert> Alerts { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<AssignmentSchedule> AssignmentSchedules { get; set; }

    public virtual DbSet<Floor> Floors { get; set; }

    public virtual DbSet<Leaf> Leaves { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Restroom> Restrooms { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<ScheduleDetail> ScheduleDetails { get; set; }

    public virtual DbSet<Sensor> Sensors { get; set; }

    public virtual DbSet<SensorBin> SensorBins { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<TrashBin> TrashBins { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<ScheduleDetailRating> ScheduleDetailRatings { get; set; }
    public virtual DbSet<ShiftSwapRequest> ShiftSwapRequests { get; set; }
    public virtual DbSet<AttendanceRecord> AttendanceRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }
    }

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .Build();
        var strConn = config["ConnectionStrings:DB"];

        return strConn;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alert>(entity =>
        {
            entity.HasKey(e => e.AlertId).HasName("PK_Alert");

            entity.Property(e => e.AlertId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.ResolvedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TimeSend).HasColumnType("datetime");
            entity.Property(e => e.TrashBinId).HasMaxLength(50);
            entity.Property(e => e.UserId).HasMaxLength(50);

            entity.HasOne(d => d.TrashBin).WithMany(p => p.Alerts)
                .HasForeignKey(d => d.TrashBinId)
                .HasConstraintName("Alerts_TrashBins_FK");

            entity.HasOne(d => d.User).WithMany(p => p.Alerts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Alerts_Users");
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.Property(e => e.AreaId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.AreaName).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.FloorId).HasMaxLength(50);
            entity.Property(e => e.IsAssigned).HasMaxLength(50);
            entity.Property(e => e.RoomBegin)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.RoomEnd)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Floor).WithMany(p => p.Areas)
                .HasForeignKey(d => d.FloorId)
                .HasConstraintName("FK_Areas_Floors");
        });

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK_Assignment");

            entity.Property(e => e.AssignmentId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.AssigmentName).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TimesPerDay).HasMaxLength(50);
        });

        modelBuilder.Entity<AssignmentSchedule>(entity =>
        {
            entity.Property(e => e.AssignmentScheduleId).HasMaxLength(50);
            entity.Property(e => e.AssignmentId).HasMaxLength(50);
            entity.Property(e => e.ScheduleId).HasMaxLength(50);

            entity.HasOne(d => d.Assignment).WithMany(p => p.AssignmentSchedules)
                .HasForeignKey(d => d.AssignmentId)
                .HasConstraintName("AssignmentSchedules_Assignments_FK");

            entity.HasOne(d => d.Schedule).WithMany(p => p.AssignmentSchedules)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("AssignmentSchedules_Schedules_FK");
        });

        modelBuilder.Entity<Floor>(entity =>
        {
            entity.HasKey(e => e.FloorId).HasName("PK_Floor");

            entity.Property(e => e.FloorId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Leaf>(entity =>
        {
            entity.HasKey(e => e.LeaveId).HasName("PK__Leaves__796DB9595BA55349");

            entity.Property(e => e.LeaveId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.ApprovalStatus).HasMaxLength(50);
            entity.Property(e => e.ApprovedBy).HasMaxLength(50);
            entity.Property(e => e.LeaveType).HasMaxLength(50);
            entity.Property(e => e.Note).HasMaxLength(50);
            entity.Property(e => e.Reason).HasMaxLength(50);
            entity.Property(e => e.WorkerId).HasMaxLength(50);

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.LeafApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK_Leaves_ApprovedBy");

            entity.HasOne(d => d.Worker).WithMany(p => p.LeafWorkers)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Leaves_Staff");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.Property(e => e.ReportId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.Image).HasMaxLength(150);
            entity.Property(e => e.Priority).HasMaxLength(50);
            entity.Property(e => e.ReportName).HasMaxLength(50);
            entity.Property(e => e.ReportType).HasMaxLength(50);
            entity.Property(e => e.ResolvedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UserId).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Reports_Users");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK_Request");

            entity.Property(e => e.RequestId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.RequestDate).HasColumnType("datetime");
            entity.Property(e => e.ResolveDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.SupervisorId).HasMaxLength(50);
            entity.Property(e => e.TrashBinId).HasMaxLength(50);
            entity.Property(e => e.WorkerId).HasMaxLength(50);

            entity.HasOne(d => d.Supervisor).WithMany(p => p.RequestSupervisors)
                .HasForeignKey(d => d.SupervisorId)
                .HasConstraintName("Requests_Users_FK");

            entity.HasOne(d => d.TrashBin).WithMany(p => p.Requests)
                .HasForeignKey(d => d.TrashBinId)
                .HasConstraintName("Requests_TrashBins_FK");

            entity.HasOne(d => d.Worker).WithMany(p => p.RequestWorkers)
                .HasForeignKey(d => d.WorkerId)
                .HasConstraintName("FK_Requests_Users");
        });

        modelBuilder.Entity<Restroom>(entity =>
        {
            entity.HasKey(e => e.RestroomId).HasName("PK_Restroom");

            entity.Property(e => e.RestroomId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.AreaId).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.RestroomNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Area).WithMany(p => p.Restrooms)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("FK_Restrooms_Areas");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_Role");

            entity.Property(e => e.RoleId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.RoleName).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK_Schedule");

            entity.Property(e => e.ScheduleId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.AreaId).HasMaxLength(50);
            entity.Property(e => e.RestroomId).HasMaxLength(50);
            entity.Property(e => e.ScheduleName).HasMaxLength(50);
            entity.Property(e => e.ScheduleType).HasMaxLength(50);
            entity.Property(e => e.ShiftId).HasMaxLength(50);
            entity.Property(e => e.TrashBinId).HasMaxLength(50);

            entity.HasOne(d => d.Area).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("FK_Schedules_Areas");

            entity.HasOne(d => d.Restroom).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.RestroomId)
                .HasConstraintName("FK_Schedules_Restrooms");

            entity.HasOne(d => d.Shift).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.ShiftId)
                .HasConstraintName("FK_Schedules_Shifts");

            entity.HasOne(d => d.TrashBin).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.TrashBinId)
                .HasConstraintName("Schedules_TrashBins_FK");
        });

        modelBuilder.Entity<ScheduleDetail>(entity =>
        {
            entity.HasKey(e => e.ScheduleDetailId).HasName("PK_ScheduleDetail");

            entity.Property(e => e.ScheduleDetailId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.BackupForUserId).HasMaxLength(50);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.EndTime).HasPrecision(0);
            entity.Property(e => e.EvidenceImage).HasMaxLength(250);
            entity.Property(e => e.IsBackup).HasMaxLength(50);
            entity.Property(e => e.Rating)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ScheduleId).HasMaxLength(50);
            entity.Property(e => e.StartTime).HasPrecision(0);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.SupervisorId).HasMaxLength(50);
            entity.Property(e => e.WorkerId).HasMaxLength(50);

            entity.HasOne(d => d.BackupForUser).WithMany(p => p.ScheduleDetailBackupForUsers)
                .HasForeignKey(d => d.BackupForUserId)
                .HasConstraintName("ScheduleDetails_Users_FK");

            entity.HasOne(d => d.Schedule).WithMany(p => p.ScheduleDetails)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK_ScheduleDetails_Schedules");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.ScheduleDetailSupervisors)
                .HasForeignKey(d => d.SupervisorId)
                .HasConstraintName("FK_ScheduleDetails_Supervisor");

            entity.HasOne(d => d.Worker).WithMany(p => p.ScheduleDetailWorkers)
                .HasForeignKey(d => d.WorkerId)
                .HasConstraintName("FK_ScheduleDetails_Worker");
        });

        modelBuilder.Entity<Sensor>(entity =>
        {
            entity.HasKey(e => e.SensorId).HasName("PK_Sensor");

            entity.Property(e => e.SensorId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.SensorName).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<SensorBin>(entity =>
        {
            entity.HasKey(e => new { e.SensorId, e.BinId });

            entity.Property(e => e.SensorId).HasMaxLength(50);
            entity.Property(e => e.BinId).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.FillLevel).HasColumnType("float");
            entity.Property(e => e.MeasuredAt).HasColumnType("datetime").HasDefaultValueSql("getdate()");

            entity.HasOne(d => d.Bin).WithMany(p => p.SensorBins)
                .HasForeignKey(d => d.BinId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SensorBins_TrashBins_FK");

            entity.HasOne(d => d.Sensor).WithMany(p => p.SensorBins)
                .HasForeignKey(d => d.SensorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SensorBins_Sensors");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.ShiftId).HasName("PK_Shift");

            entity.Property(e => e.ShiftId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.ShiftName).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<TrashBin>(entity =>
        {
            entity.HasKey(e => e.TrashBinId).HasName("PK_TrashBin");

            entity.Property(e => e.TrashBinId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.AreaId).HasMaxLength(50);
            entity.Property(e => e.Image).HasMaxLength(150);
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.RestroomId).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Area).WithMany(p => p.TrashBins)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("TrashBins_Areas_FK");

            entity.HasOne(d => d.Restroom).WithMany(p => p.TrashBins)
                .HasForeignKey(d => d.RestroomId)
                .HasConstraintName("TrashBins_Restrooms_FK");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User");

            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Image)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReasonForLeave).HasMaxLength(50);
            entity.Property(e => e.RoleId).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<ScheduleDetailRating>(entity =>
        {
            entity.HasKey(e => e.ScheduleDetailRatingId).HasName("PK_ScheduleDetailRating");

            entity.Property(e => e.ScheduleDetailRatingId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.ScheduleDetailId).HasMaxLength(50);
            entity.Property(e => e.RatedByUserId).HasMaxLength(50);
            entity.Property(e => e.RatingDate).HasColumnType("date");
            entity.Property(e => e.RatedAt).HasColumnType("datetime");
            entity.Property(e => e.Comment).HasMaxLength(200);
            entity.Property(e => e.RatingValue).HasColumnType("int");

            entity.HasOne(d => d.ScheduleDetail)
                .WithMany(p => p.Ratings)
                .HasForeignKey(d => d.ScheduleDetailId)
                .HasConstraintName("FK_ScheduleDetailRatings_ScheduleDetail");

            entity.HasIndex(e => new { e.ScheduleDetailId, e.RatingDate }).IsUnique();
        });

        modelBuilder.Entity<ShiftSwapRequest>(entity =>
        {
            entity.HasKey(e => e.SwapRequestId).HasName("PK_ShiftSwapRequest");

            entity.Property(e => e.SwapRequestId)
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.RequestDate)
                .IsRequired();

            entity.Property(e => e.RequesterId)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.TargetUserId)
                .HasMaxLength(50);

            entity.Property(e => e.TargetUserPhone)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Reason)
                .HasMaxLength(500);

            entity.Property(e => e.Month)
                .IsRequired();

            entity.Property(e => e.Year)
                .IsRequired();

   
            entity.HasOne(d => d.Requester)
                .WithMany()
                .HasForeignKey(d => d.RequesterId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ShiftSwapRequest_Requester");

            entity.HasOne(d => d.TargetUser)
                .WithMany()
                .HasForeignKey(d => d.TargetUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ShiftSwapRequest_TargetUser");
        });
        modelBuilder.Entity<AttendanceRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AttendanceRecord");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(50);

            entity.Property(e => e.AttendanceDate)
                .HasColumnType("date");

            entity.Property(e => e.CheckInTime)
                .HasColumnType("datetime");

            entity.Property(e => e.CheckOutTime)
                .HasColumnType("datetime");

            entity.Property(e => e.Status)
                .HasMaxLength(50);

            entity.Property(e => e.Note)
                .HasMaxLength(500);

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime");

            entity.HasOne(e => e.User)
                .WithMany(u => u.AttendanceRecords)
                .HasForeignKey(e => e.EmployeeId)
                .HasConstraintName("FK_AttendanceRecords_Users");
        });

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
