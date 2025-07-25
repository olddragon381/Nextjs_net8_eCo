import { useState } from 'react';
import RangeSlider from 'react-range-slider-input';
import 'react-range-slider-input/dist/style.css';

const PriceDropdown = ({ maxPrice,selectedPrice, setSelectedPrice }) => {
  const [toggleDropdown, setToggleDropdown] = useState(true);
  
  return (
    <div className="bg-white shadow-1 rounded-lg">
      {/* Dropdown toggle */}
      <div
        onClick={() => setToggleDropdown(!toggleDropdown)}
        className="cursor-pointer flex items-center justify-between py-3 pl-6 pr-5.5"
      >
        <p className="text-dark">Giá</p>
        <button
          onClick={() => setToggleDropdown(!toggleDropdown)}
          className={`text-dark ease-out duration-200 ${
            toggleDropdown && 'rotate-180'
          }`}
        >
          {/* icon */}
        </button>
      </div>

      {/* Dropdown content */}
      <div className={`p-6 ${toggleDropdown ? 'block' : 'hidden'}`}>
        <div className="price-range">
         <RangeSlider
  min={0}
  max={maxPrice}
  value={[selectedPrice.from, selectedPrice.to]} // ← thay vì defaultValue
  step={1}
  onInput={(e) =>
    setSelectedPrice({
      from: Math.floor(e[0]),
      to: Math.ceil(e[1]),
    })
  }

/>

          {/* Hiển thị giá */}
          <div className="price-amount flex items-center justify-between pt-4">
            <div className="text-dark-4 flex rounded border px-2 py-1.5">
        
              <span className="ml-2">{selectedPrice.from}</span>
            </div>
            <div className="text-dark-4 flex rounded border px-2 py-1.5">
           
              <span className="ml-2">{selectedPrice.to}</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default PriceDropdown;
