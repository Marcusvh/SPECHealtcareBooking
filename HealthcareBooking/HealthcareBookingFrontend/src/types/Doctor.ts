export interface Doctor {
  specialties: string;
  medicalLincenseNumber: string;
  yearsOfExperience: number;
  isAcceptingNewPatients: boolean;
  assignedDepartment: string;
  staffId: string;
  name: string;
  description: string;
  type: "Doctor"; // for validation
  supportedBookingTypeIds: string[];
  supportedBookingTypes: string[];
}
