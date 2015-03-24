function colourItemMapper (colorItem) {
			return (
				React.createElement(Color, {
					backgroundName: colorItem.backgroundName, 
					backgroundColour: colorItem.backgroundColour, 
					foregroundName: colorItem.foregroundName, 
					foregroundColour: colorItem.foregroundColour, 
					foregroundOpacity: colorItem.foregroundOpacity})
			);
		}

var Color = React.createClass({displayName: "Color",
	render: function() {

		var bgStyle = {
			background:this.props.backgroundColour			
		};
		var fgStyle = {
			color:this.props.foregroundColour,
			opacity:this.props.foregroundOpacity
		};

		return (
			React.createElement("div", {className: "swatchColor", style: bgStyle}, 
				React.createElement("span", {style: fgStyle}, this.props.backgroundName)
			)
		);
	}
});

var SwatchSelector = React.createClass({displayName: "SwatchSelector",
	handleUseClick: function(isAccent, e) {
		e.preventDefault();		
		this.props.onUseClick(isAccent, e);
	},
	render: function() {
		return (
			React.createElement("div", null, "Use ", React.createElement("a", {href: "#", onClick: this.handleUseClick.bind(this, false)}, "Primary"), " ", React.createElement("a", {href: "#", onClick: this.handleUseClick.bind(this, true)}, "Accent"))
		);
	}
});

var Swatch = React.createClass({displayName: "Swatch",
	handleUseClick: function(index, isAccent, e) {		
		this.props.onUseClick(index, isAccent, e);		
	},
	render: function() {
		var colorNodes = this.props.colors.map(colourItemMapper);		
		return (
			React.createElement("div", {className: "swatch"}, 
				this.props.name, 
				React.createElement(SwatchSelector, {onUseClick: this.handleUseClick.bind(this, this.props.index)}), 
				colorNodes
			)
		);
	}
});

var SwatchesBox = React.createClass({displayName: "SwatchesBox",
	handleUseClick: function(index, isAccent, e) {
		this.props.onUseClick(index, isAccent);		
	},
	render: function() {
		var swatchNodes = this.props.swatches.map(function(swatch, index) {
			return (
				React.createElement(Swatch, {name: swatch.name, colors: swatch.colors, index: index, onUseClick: this.handleUseClick})
			);
		}, this);

		return (
			React.createElement("div", null, 
				swatchNodes
			)
		);
	}
});

var HuesBox = React.createClass({displayName: "HuesBox",
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
			React.createElement("div", null, 				
				primaryColourNodes, 
				accentColourNodes
			)
		);
	}
});

var huesBox = React.render(
	React.createElement(HuesBox, null),
	document.getElementById('hues')
	);

function mergeSwatch(index, isAccent) {	
	var newSwatch = (isAccent ? { accentSwatch: swatches[index]} : { primarySwatch: swatches[index]});
	huesBox.setState(newSwatch);
}

React.render(
	React.createElement(SwatchesBox, {swatches: swatches, onUseClick: mergeSwatch}),
	document.getElementById('swatches')
	);
