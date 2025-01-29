import React from "react";
import "./Card.css";

export const Card = () => {
    return (
        <div className="card">
            <div className="card-info">
                <div className="card-profile-pic">
                    Profile Pic
                    <div></div>
                </div>
                <div className="card-general">
                    General Info
                    <div></div>
                </div>
                <div className="card-office-hour">
                    Office Hour
                    <div></div>
                </div>
            </div>
            <div className="card-footer"></div>
        </div>
    );
};
