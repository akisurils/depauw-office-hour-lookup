import Image from "next/image";

export default function Home() {
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
            <div className="grid grid-rows-[20px_1fr_20px] items-center justify-items-center min-h-screen p-8 pb-20 gap-16 sm:p-20 font-[family-name:var(--font-geist-sans)]"></div>
        </div>
    );
}
