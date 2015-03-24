function colourItemMapper (colorItem) {
			return (
				<Color 
					backgroundName={colorItem.backgroundName} 
					backgroundColour={colorItem.backgroundColour} 
					foregroundName={colorItem.foregroundName} 
					foregroundColour={colorItem.foregroundColour} 
					foregroundOpacity={colorItem.foregroundOpacity} />
			);
		}

var Color = React.createClass({
	render: function() {

		var bgStyle = {
			background:this.props.backgroundColour			
		};
		var fgStyle = {
			color:this.props.foregroundColour,
			opacity:this.props.foregroundOpacity
		};

		return (
			<div className="swatchColor" style={bgStyle}>
				<span style={fgStyle}>{this.props.backgroundName}</span>
			</div>
		);
	}
});

var SwatchSelector = React.createClass({
	handleUseClick: function(isAccent, e) {
		e.preventDefault();		
		this.props.onUseClick(isAccent, e);
	},
	render: function() {
		return (
			<div>Use <a href="#" onClick={this.handleUseClick.bind(this, false)}>Primary</a> <a href="#" onClick={this.handleUseClick.bind(this, true)}>Accent</a></div>
		);
	}
});

var Swatch = React.createClass({
	handleUseClick: function(index, isAccent, e) {		
		this.props.onUseClick(index, isAccent, e);		
	},
	render: function() {
		var colorNodes = this.props.colors.map(colourItemMapper);		
		return (
			<div className="swatch">
				{this.props.name}
				<SwatchSelector onUseClick={this.handleUseClick.bind(this, this.props.index)} />
				{colorNodes}
			</div>
		);
	}
});

var SwatchesBox = React.createClass({
	handleUseClick: function(index, isAccent, e) {
		this.props.onUseClick(index, isAccent);		
	},
	render: function() {
		var swatchNodes = this.props.swatches.map(function(swatch, index) {
			return (
				<Swatch name={swatch.name} colors={swatch.colors} index={index} onUseClick={this.handleUseClick} />
			);
		}, this);

		return (
			<div>
				{swatchNodes}
			</div>
		);
	}
});

var HuesBox = React.createClass({
	getInitialState: function() {
		return {
			primarySwatch: swatches[0],
			accentSwatch: swatches[5]
		}
	},	
	render: function() {
		var primaryColours = this.state.primarySwatch.colors.filter(function(colorItem) {
			return colorItem.backgroundName.lastIndexOf("Primary") === 0;
		});
		var accentColours = this.state.accentSwatch.colors.filter(function(colorItem) {
			return colorItem.backgroundName.lastIndexOf("Accent") === 0;
		});
		var primaryColourNodes = primaryColours.map(colourItemMapper);
		var accentColourNodes = accentColours.map(colourItemMapper);
		return (
			<div>				
				{primaryColourNodes}
				{accentColourNodes}
			</div>
		);
	}
});

var huesBox = React.render(
	<HuesBox />,
	document.getElementById('hues')
	);

function mergeSwatch(index, isAccent) {	
	var newSwatch = (isAccent ? { accentSwatch: swatches[index]} : { primarySwatch: swatches[index]});
	huesBox.setState(newSwatch);
}

React.render(
	<SwatchesBox swatches={swatches} onUseClick={mergeSwatch} />,
	document.getElementById('swatches')
	);
