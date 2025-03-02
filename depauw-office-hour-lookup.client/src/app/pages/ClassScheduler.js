import Link from "next/link";
import React from "react";
import "./ClassScheduler.css";

const ClassScheduler = () => {
    return (
        <div className="bg">
            <div className="header">
                HEADER
                <div className="subheader">Hello wuốt</div>
            </div>

            <Link href="/officehourlookup">OFFICE HOUR LOOKUP</Link>
            <div className="flex flex-col bg-lime-500 h-[50%] p-4">
                <div className="flex">
                    <div className="w-20"></div>
                    <div className="flex items-center">
                        <div className="w-32 text-center border border-slate-950">
                            Mon
                        </div>
                        <div className="w-32 text-center h-auto">Tue</div>
                        <div className="w-32 text-center">Wed</div>
                        <div className="w-32 text-center">Thu</div>
                        <div className="w-32 text-center">Fri</div>
                    </div>
                </div>
                <div className="flex flex-1">
                    <div className="flex flex-col w-20 border border-slate-950 items-center justify-between ">
                        <div>8:00</div>
                        <div>9:00</div>
                        <div>10:00</div>
                        <div>11:00</div>
                        <div>12:00</div>
                        <div>1:00</div>
                        <div>2:00</div>
                        <div>3:00</div>
                        <div>4:00</div>
                    </div>
                    <div className="flex">
                        <div className="calender-cell border border-slate-950">
                            Mon
                        </div>
                        <div className="calender-cell border border-slate-950">
                            Tue
                        </div>
                        <div className="calender-cell border border-slate-950">
                            Wed
                        </div>
                        <div className="calender-cell border border-slate-950">
                            Thu
                        </div>
                        <div className="calender-cell border border-slate-950">
                            Fri
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default ClassScheduler;
