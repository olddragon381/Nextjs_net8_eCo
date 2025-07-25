import React from "react";
import Hero from "./Hero";
import Categories from "./Categories";
import NewArrival from "./NewArrivals";

import BestSeller from "./BestSeller";

import Testimonials from "./Testimonials";


const Home = () => {
  return (
    <main>
      <Hero />
      <Categories />
      <NewArrival />
      
      {/* <BestSeller /> */}
   
      <Testimonials />
    
    </main>
  );
};

export default Home;
