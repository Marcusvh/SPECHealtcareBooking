interface btnProps {
    title: string;
    onClick?: (e: React.MouseEvent<HTMLButtonElement>) => void;
}

document.addEventListener('click', function (e) {
  const target = e.target as HTMLElement;
    if(target.tagName === 'BUTTON' && target.classList.contains('bookingTimebtnActive')) {
        target.classList.remove('bookingTimebtnActive');
        return;
    }
    if (target.tagName === 'BUTTON' && target.classList.contains('bookingTimebtn')) {
        Array.from(document.getElementsByClassName('bookingTimebtn')).forEach((btn) => {
            btn.classList.remove('bookingTimebtnActive');
        });
        target.classList.toggle('bookingTimebtnActive');
    }
});

const BookingTimeBtn: React.FC<btnProps> = ({ title, onClick }) => {
  return (
    <button onClick={onClick} className="py-2.5 bg-gray-800 w-[16%] text-gray-50 text-center hover:bg-rose-700 bookingTimebtn"> {title} </button>
  )
}

export default BookingTimeBtn;