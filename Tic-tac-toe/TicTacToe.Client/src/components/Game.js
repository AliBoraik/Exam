import React from 'react';

const Game = (props) => (
    <div style={{ background: "#eee", borderRadius: '5px', padding: '0 10px' }}>
        <p><strong>{props.id}</strong></p>
    </div>
);

export default Game;