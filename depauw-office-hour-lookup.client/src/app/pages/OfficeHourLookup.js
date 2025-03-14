import React from "react";
import Card from "../components/Card";

export const OfficeHourLookup = () => {
    return (
        <div className="bg">
            <div className="header">
                <div>DEPAUW | OFFICE HOUR LOOKUP</div>
                <div className="subheader">
                    <a href="https://github.com/akisurils">SUGGESTION</a>
                    <div className="separator">|</div>
                    <a href="https://github.com/akisurils">PROFESSOR</a>
                </div>
            </div>
            <div className="searchbar">
                <div className="search-title">Faculty Name</div>
                <input type="text" className="search-form" />
                <button className="search-button">Search</button>
            </div>
            <div>
                Filter
                <div>Department</div>
                <div></div>
            </div>
            <Card />
        </div>
    );
};
