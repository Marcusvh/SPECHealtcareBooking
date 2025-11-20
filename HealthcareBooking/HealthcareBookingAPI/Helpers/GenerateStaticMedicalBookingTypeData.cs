using HealthcareModels.Models.HealthcareStaff;

namespace HealthcareBookingAPI.Helpers
{
    public static class GenerateStaticMedicalBookingTypeData
    {
        public static readonly List<MedicalBookingType> MedicalBookingTypes = new()
        {
            // ---------------------------------------
            // GENERAL MEDICINE
            // ---------------------------------------
            new() {
                Name = "General Consultation",
                Description = "Routine health check or medical concern.",
                RequiredStaffType = StaffType.Doctor,
                DurationMinutes = 20,
                Department = "General Medicine",
                AllowedSpecialties = new() { "General Medicine", "Family Medicine" }
            },
            new() {
                Name = "Chronic Condition Follow-up",
                Description = "Follow-up visit for chronic conditions.",
                RequiredStaffType = StaffType.Doctor,
                DurationMinutes = 15,
                Department = "General Medicine",
                AllowedSpecialties = new() { "General Medicine" }
            },
            new() {
                Name = "Blood Pressure Check",
                Description = "Routine blood pressure measurement.",
                RequiredStaffType = StaffType.Nurse,
                DurationMinutes = 10,
                Department = "General Medicine",
                AllowedSpecialties = new() { "General Medicine" }
            },

            // ---------------------------------------
            // PEDIATRICS
            // ---------------------------------------
            new() {
                Name = "Pediatric Consultation",
                Description = "Consultation for children under 18.",
                RequiredStaffType = StaffType.Doctor,
                DurationMinutes = 25,
                Department = "Pediatrics",
                AllowedSpecialties = new() { "Pediatrics" }
            },
            new() {
                Name = "Childhood Vaccination",
                Description = "Scheduled child vaccination.",
                RequiredStaffType = StaffType.Nurse,
                DurationMinutes = 15,
                Department = "Pediatrics",
                AllowedSpecialties = new() { "Pediatrics" }
            },

            // ---------------------------------------
            // CARDIOLOGY
            // ---------------------------------------
            new() {
                Name = "Cardiology Consultation",
                Description = "Heart-related examination.",
                RequiredStaffType = StaffType.Doctor,
                DurationMinutes = 30,
                Department = "Cardiology",
                AllowedSpecialties = new() { "Cardiology" }
            },
            new() {
                Name = "EKG Test",
                Description = "Electrocardiogram procedure.",
                RequiredStaffType = StaffType.Nurse,
                DurationMinutes = 15,
                Department = "Cardiology",
                AllowedSpecialties = new() { "Cardiology" }
            },

            // ---------------------------------------
            // DERMATOLOGY
            // ---------------------------------------
            new() {
                Name = "Skin Examination",
                Description = "Evaluation of dermatological concerns.",
                RequiredStaffType = StaffType.Doctor,
                DurationMinutes = 20,
                Department = "Dermatology",
                AllowedSpecialties = new() { "Dermatology" }
            },
            new() {
                Name = "Dermatology Follow-up",
                Description = "Follow-up for ongoing skin treatment.",
                RequiredStaffType = StaffType.Nurse,
                DurationMinutes = 15,
                Department = "Dermatology",
                AllowedSpecialties = new() { "Dermatology" }
            },

            // ---------------------------------------
            // ORTHOPEDICS
            // ---------------------------------------
            new() {
                Name = "Orthopedic Evaluation",
                Description = "Assessment of injuries or joint pain.",
                RequiredStaffType = StaffType.Doctor,
                DurationMinutes = 25,
                Department = "Orthopedics",
                AllowedSpecialties = new() { "Orthopedics" }
            },
            new() {
                Name = "Cast Removal",
                Description = "Removal of orthopedic cast.",
                RequiredStaffType = StaffType.Nurse,
                DurationMinutes = 20,
                Department = "Orthopedics",
                AllowedSpecialties = new() { "Orthopedics" }
            },

            // ---------------------------------------
            // PSYCHIATRY
            // ---------------------------------------
            new() {
                Name = "Psychiatric Assessment",
                Description = "Initial mental health evaluation.",
                RequiredStaffType = StaffType.Doctor,
                DurationMinutes = 60,
                Department = "Psychiatry",
                AllowedSpecialties = new() { "Psychiatry" }
            },
            new() {
                Name = "Therapy Session",
                Description = "Follow-up psychological therapy.",
                RequiredStaffType = StaffType.Doctor,
                DurationMinutes = 45,
                Department = "Psychiatry",
                AllowedSpecialties = new() { "Psychiatry" }
            },

            // ---------------------------------------
            // ONCOLOGY
            // ---------------------------------------
            new() {
                Name = "Oncology Consultation",
                Description = "Cancer diagnosis or treatment consultation.",
                RequiredStaffType = StaffType.Doctor,
                DurationMinutes = 40,
                Department = "Oncology",
                AllowedSpecialties = new() { "Oncology" }
            },
        };

    }
}
