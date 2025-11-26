import { useState, type FormEvent } from "react";

export const Contact = () => {
  const [formData, setFormData] = useState({
    name: "",
    email: "",
    message: "",
  });

  const [submitted, setSubmitted] = useState(false);

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();
    // TODO: connect to backend or service like EmailJS / API endpoint
    console.log("Form submitted:", formData);
    setSubmitted(true);

    setTimeout(() => setSubmitted(false), 3000);
  };

  return (
    <div className="min-h-screen bg-white text-gray-700 py-16 px-6">
      <div className="max-w-5xl mx-auto">

        {/* Header */}
        <h1 className="text-4xl font-bold text-rose-600 text-center mb-10">
          Contact Us
        </h1>
        <p className="text-center text-gray-500 mb-12">
          If you have questions about booking, appointments, or medical records,
          feel free to reach out using the information or form below.
        </p>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-12">

          {/* Contact Info */}
          <div className="space-y-6">
            <h2 className="text-2xl font-semibold">Contact Information</h2>

            <div>
              <p className="font-medium">📞 Phone</p>
              <p className="text-gray-500">+1 234 567 890</p>
            </div>

            <div>
              <p className="font-medium">📧 Email</p>
              <p className="text-gray-500">support@patientbookingsystem.com</p>
            </div>

            <div>
              <p className="font-medium">📍 Address</p>
              <p className="text-gray-500">
                123 Healthcare Avenue <br />
                Medical District <br />
                14000, Mushroom island
              </p>
            </div>

            <div>
              <p className="font-medium">⏱ Opening Hours</p>
              <p className="text-gray-500">Mon-Fri: 08:00 - 17:00</p>
              <p className="text-gray-500">Sat: 10:00 - 14:00</p>
              <p className="text-gray-500">Sun: Closed</p>
            </div>

            {/* Map Placeholder */}
            <div className="mt-6 h-48 bg-gray-200 rounded-lg flex flex-col items-center justify-center">
              <p className="text-gray-500">Map Integration (Google Maps)</p>
              <p>if i got the time and prio for it</p>
            </div>
          </div>

          {/* Contact Form */}
          <div className="bg-rose-50/50 border border-rose-600 p-8 rounded-lg shadow-sm">
            <h2 className="text-2xl font-semibold mb-6">Send us a Message</h2>

            {submitted && (
              <div className="mb-4 p-3 bg-green-100 text-green-700 text-sm rounded">
                Your message has been sent!
              </div>
            )}

            <form onSubmit={handleSubmit} className="space-y-5">
              <div>
                <label className="block font-medium mb-1">Full Name</label>
                <input
                  name="name"
                  type="text"
                  required
                  value={formData.name}
                  onChange={handleChange}
                  className="w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-rose-300"
                />
              </div>

              <div>
                <label className="block font-medium mb-1">Email</label>
                <input
                  name="email"
                  type="email"
                  required
                  value={formData.email}
                  onChange={handleChange}
                  className="w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-rose-300"
                />
              </div>

              <div>
                <label className="block font-medium mb-1">Message</label>
                <textarea
                  name="message"
                  required
                  rows={4}
                  value={formData.message}
                  onChange={handleChange}
                  className="w-full px-4 py-2 border rounded-lg focus:ring-2 focus:ring-rose-300"
                ></textarea>
              </div>

              <button
                type="submit"
                className="w-full bg-rose-600 hover:bg-rose-700 text-white py-2 rounded-lg transition"
              >
                Send Message
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
}
