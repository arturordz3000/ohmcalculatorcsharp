import './BandColorSelector.css'

function BandColorSelector(props) {
    const configuration = props.configuration;
    const bandColorSelectors = [];

    for (const [index, value] of configuration.entries()) {
        const rowIndex = index;
        const row = value;
        const colors = [];

        for (const [index, value] of row.colors.entries()) {
            const colorIndex = index;
            const color = value;
            colors.push(
                <div 
                    key={index}
                    onClick={() => { props.onColorSelected(rowIndex, colorIndex) }}
                    title={color.valueDescription} 
                    className="BandColorSelector-option" 
                    style={{ backgroundColor: color.rgb, width: (80 / row.colors.length) + '%' }}>
                        <p>{color.valueDescription}</p>
                </div>);
        }
        
        bandColorSelectors.push(<div key={index} className="BandColorSelector-row"><p>{row.name}</p><div>{colors}</div></div>);
    }

    return (
        <div className="BandColorSelector">
            {bandColorSelectors}
        </div>
    );
}

export default BandColorSelector;