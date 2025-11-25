interface Props {
    patient: any;
    onClose: () => void;
}

export const ContactPatientModal: React.FC<Props> = ({ patient, onClose }) => {
    return (
    <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">
      <div className="bg-white rounded-lg shadow-lg p-6 w-full max-w-md">
        
        {/* Header */}
        <div className="flex justify-between items-center mb-4">
          <h2 className="text-xl font-semibold">Contact Patient</h2>
          <button onClick={onClose} className="text-gray-500 hover:text-gray-700 text-xl">
            ✕
          </button>
        </div>

        {/* Patient Info */}
        <div className="space-y-2">
          <p><span className="font-medium">Name:</span> {patient.patientFullName}</p>
          <p><span className="font-medium">Phone:</span> {patient.patientPhoneNumber}</p>
          <p><span className="font-medium">Email:</span> {patient.patientEmail}</p>
        </div>

        {/* Actions */}
        <div className="mt-6 space-y-3">
          <a
            href={`tel:${patient.patientPhoneNumber}`}
            className="block w-full text-center bg-blue-600 text-white py-2 rounded-lg hover:bg-blue-700"
          >
            Call Patient
          </a>

          <a
            href={`mailto:${patient.patientEmail}`}
            className="block w-full text-center bg-green-600 text-white py-2 rounded-lg hover:bg-green-700"
          >
            Email Patient
          </a>

          <button
            className="w-full border border-gray-400 text-gray-700 py-2 rounded-lg hover:bg-gray-200"
            onClick={onClose}
          >
            Cancel
          </button>
        </div>

      </div>
    </div>
  );
};