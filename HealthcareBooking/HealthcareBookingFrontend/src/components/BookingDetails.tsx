import { useState } from "react";
import { EditableTextArea } from "./EditableTextArea";
import type { BookingView } from "../types/bookingView";
import {ContactPatientModal} from "./ContactPatientModal";
import { apiRequest } from "../apiRequest/HealthcareApi";

interface Props {
  booking: BookingView | any;
  needsAction?: boolean;
  onConfirm?: () => void;
  onReject?: () => void;
}

const BookingDetails: React.FC<Props> = ({ booking, needsAction = true, onConfirm, onReject }) => {
  const [showContactModal, setShowContactModal] = useState(false);
    const handleSaveStaffNotes = (newText: string) => {
      console.log("save staffnote: " + newText);
      apiRequest(`Booking/booking/id/${booking.detailedBookingViewid}/staffNote`, "Patch", newText )
      .then(() => {
        console.log("Saved staff notes:", newText);
      })
      .catch((error) => {
        console.error("Error updating staff notes:", error);
      });
  };

  return (
    <div className="max-w-2xl mx-auto bg-white p-6 rounded-xl shadow-lg shadow-gray-300 border">
      <h2 className="text-2xl font-semibold text-gray-900 mb-6">
        Booking Details
      </h2>

      {/* Patient Info */}
      <div className="mb-6">
        <h2 className="text-lg font-medium mb-2 text-gray-800">Patient</h2>
        <p><span className="font-medium">Name: </span> {booking?.patientFullName}</p>
        <p><span className="font-medium">Date of Birth: </span> {new Date(booking?.dob).toLocaleDateString()}</p>
      </div>

      {/* Booking Info */}
      <div className="mb-6">
        <h2 className="text-lg font-medium mb-2 text-gray-800">Booking Info</h2>

        <p>
          <span className="font-medium">Status: </span>
          <span className={`px-2 py-1 rounded text-sm 
            ${booking?.bookingStatus === "Scheduled" ? "bg-yellow-100 text-yellow-700" : ""}
            ${booking?.bookingStatus === "Completed" ? "bg-green-100 text-green-700" : ""}
            ${booking?.bookingStatus === "Cancelled" ? "bg-red-100 text-red-700" : ""}
          `}>
            {booking?.bookingStatus}
          </span>
        </p>
        <p>
          <span className="font-medium">Booking Confirmation Status: </span>
          <span className={`px-2 py-1 rounded text-sm 
            ${booking?.bookingStage === "Second" ? "bg-yellow-100 text-yellow-700" : ""}
            ${booking?.bookingStage === "Confirmed" ? "bg-green-100 text-green-700" : ""}
            ${booking?.bookingStage === "Cancelled" ? "bg-red-100 text-red-700" : ""}
          `}>
            {booking?.bookingStage === "Second" ? "Pending Confirmation" : booking?.bookingStage}
          </span>
        </p>

        <p><span className="font-medium">Start Time: </span> 
          {new Date(booking?.startTime).toLocaleString()}
        </p>

        <p><span className="font-medium">Booking Type: </span> 
          {booking?.bookingTypeName || "N/A"}
        </p>

        <p><span className="font-medium">Duration: </span> 
          {booking?.duration ?? "30 mins"}
        </p>
      </div>

      {/* Notes */}
      <div className="mb-6">
        <h2 className="text-lg font-medium mb-2 text-gray-800">Notes</h2>

        {/* Patient Notes */}
        <div className="mb-4">
          <span className="font-medium">Patient Notes:</span>
          {booking?.patientNotes ? (
            <textarea
              disabled={true}
              className="mt-2 w-full border border-gray-300 rounded-lg p-2 bg-gray-50"
              readOnly
              value={booking.patientNotes}
            />
          ) : (
            <p className="mt-1 text-gray-600">None</p>
          )}
        </div>

        {/* Staff Notes */}
        <div>
          <span className="font-medium">Staff Notes:</span>
          <EditableTextArea value={booking?.staffNotes || ""} onSave={handleSaveStaffNotes}></EditableTextArea>
        </div>
      </div>
      
      {/* Actions */}
      {needsAction ? (
      <div className="flex gap-3 mt-6 justify-center">
        <button className="px-4 py-2 bg-green-500 text-white rounded-lg hover:bg-green-600"
          onClick={onConfirm}>
          Confirm
        </button>
        <button className="px-4 py-2 bg-rose-600 text-white rounded-lg hover:bg-rose-700"
          onClick={onReject}>
          Reject
        </button>
        <button
          onClick={() => setShowContactModal(true)}
          className="px-4 py-2 border border-gray-300 text-gray-700 rounded-lg hover:bg-gray-200"
        >
          Contact Patient
        </button>

        {showContactModal && (
          <ContactPatientModal patient={booking} onClose={() => setShowContactModal(false)} />
        )}

      </div>
      ) : null}
    </div>
  );
};

export default BookingDetails;
