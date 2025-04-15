import React from 'react';

const ProgressBar = ({ percent }) => {
    return (
        <div className="progress-bar">
            <div className="fill" style={{ width: `${percent}%` }}></div>
        </div>
    );
};

export default ProgressBar;