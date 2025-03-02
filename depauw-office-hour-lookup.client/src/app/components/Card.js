import React from "react";
import "./Card.css";
import Image from "next/image";

const Card = () => {
    return (
        <div className="card">
            <div className="card-info">
                <div className="card-profile-pic">
                    <Image
                        src="https://my.depauw.edu/e/cis/csimages/000726034.jpg"
                        alt="empty profile pic"
                        width={40}
                        height={100}
                    />
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

export default Card;
