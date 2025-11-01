using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Assesment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Appointments",
                newName: "Specialization");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Appointments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PatientName",
                table: "Appointments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Availability", "Biography", "ProfilePictureUrl" },
                values: new object[] { "Monday-Friday, 9:00 AM - 5:00 PM", "Experienced cardiologist with 15 years of practice. Specializes in heart disease prevention and treatment.", "https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?w=400&h=400&fit=crop&crop=face" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Availability", "Biography", "ProfilePictureUrl" },
                values: new object[] { "Monday-Wednesday, 8:00 AM - 4:00 PM", "Pediatric specialist focused on child healthcare and developmental milestones.", "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=400&h=400&fit=crop&crop=face" });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Availability", "Biography", "FullName", "ProfilePictureUrl", "Specialization" },
                values: new object[,]
                {
                    { 3, "Tuesday-Thursday, 10:00 AM - 6:00 PM", "Dermatologist specializing in skin conditions, acne treatment, and cosmetic dermatology.", "Dr. Ahmed Karim", "https://images.unsplash.com/photo-1582750433449-648ed127bb54?w=400&h=400&fit=crop&crop=face", "Dermatology" },
                    { 4, "Monday-Thursday, 8:30 AM - 4:30 PM", "Neurologist with expertise in migraine treatment and neurological disorders.", "Dr. Maria Rodriguez", "https://images.unsplash.com/photo-1594824947933-d0501ba2fe65?w=400&h=400&fit=crop&crop=face", "Neurology" },
                    { 5, "Monday-Friday, 7:00 AM - 3:00 PM", "Orthopedic surgeon specializing in sports injuries and joint replacement surgeries.", "Dr. James Wilson", "https://images.unsplash.com/photo-1622253692010-333f2da6031d?w=400&h=400&fit=crop&crop=face", "Orthopedics" },
                    { 6, "Tuesday-Friday, 9:00 AM - 5:00 PM", "Psychiatrist focusing on anxiety disorders, depression, and cognitive behavioral therapy.", "Dr. Lisa Chen", "https://images.unsplash.com/photo-1591604021695-0c69b7c05981?w=400&h=400&fit=crop&crop=face", "Psychiatry" },
                    { 7, "Monday-Wednesday-Friday, 8:00 AM - 4:00 PM", "Gastroenterologist with expertise in digestive disorders and endoscopic procedures.", "Dr. Robert Brown", "https://images.unsplash.com/photo-1537368910025-700350fe46c7?w=400&h=400&fit=crop&crop=face", "Gastroenterology" },
                    { 8, "Monday-Thursday, 9:00 AM - 5:00 PM", "OB/GYN providing comprehensive women's health services and prenatal care.", "Dr. Emily Davis", "https://images.unsplash.com/photo-1551601651-2a8555f1a136?w=400&h=400&fit=crop&crop=face", "Obstetrics & Gynecology" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_PatientId",
                table: "Appointments");

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PatientName",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Specialization",
                table: "Appointments",
                newName: "Note");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Availability", "Biography", "ProfilePictureUrl" },
                values: new object[] { "Mon-Fri 09:00-17:00", "Experienced cardiologist specializing in adult heart health.", "https://example.com/john-smith.jpg" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Availability", "Biography", "ProfilePictureUrl" },
                values: new object[] { "Tue-Sat 10:00-18:00", "Passionate pediatrician with 10+ years of experience caring for children.", "https://example.com/sarah-johnson.jpg" });

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
