import { useState } from "react";

interface EditableTextAreaProps {
  value: string;
  onSave: (newValue: string) => void;
}

export const EditableTextArea: React.FC<EditableTextAreaProps> = ({ value, onSave }) => {
  const [isEditing, setIsEditing] = useState(false);
  const [text, setText] = useState(value || "");

  const handleSave = () => {
    onSave(text);
    setIsEditing(false);
  };

  const handleCancel = () => {
    setText(value || ""); // revert changes
    setIsEditing(false);
  };

  return (
    <div className="mb-4 mt-2 flex flex-row-reverse justify-between">
      <div className="flex items-center justify-between text-align-center">
        {!isEditing && (
          <button
            className="text-blue-600 hover:underline text-sm "
            onClick={() => setIsEditing(true)}
          >
            Make an edit
          </button>
        )}
      </div>

      {isEditing ? (
        <div className="w-full">
          <textarea
            className="w-full min-h-[10vh] border border-gray-300 rounded-lg p-2 bg-white"
            value={text}
            onChange={(e) => setText(e.target.value)}
            rows={4}
          />
          <div className="mt-2 flex gap-2">
            <button
              className="bg-blue-600 text-white px-4 py-1 rounded-lg hover:bg-blue-700"
              onClick={handleSave}
            >
              Save
            </button>
            <button
              className="border border-gray-400 px-4 py-1 rounded-lg hover:bg-gray-200"
              onClick={handleCancel}
            >
              Cancel
            </button>
          </div>
        </div>
      ) : (
        <div className="w-full min-h-[10vh]">
          {value && value.trim() !== "" ? 
            (<textarea className="w-full h-full border border-gray-300 rounded-lg p-2 bg-gray-50" value={value} disabled={true} />)
            : "None"}
        </div>
      )}
    </div>
  );
}
