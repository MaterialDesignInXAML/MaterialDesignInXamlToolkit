var Color = React.createClass({displayName: "Color",
	render: function() {

		var style = {
			background:this.props.backgroundColour,
			color:this.props.foregroundColour
		};

		return (
			React.createElement("div", {style: style}, this.props.backgroundName)
		);
	}
});

var Swatch = React.createClass({displayName: "Swatch",
	render: function() {
		var colorNodes = this.props.colors.map(function (colorItem) {
			return (
				React.createElement(Color, {backgroundName: colorItem.backgroundName, backgroundColour: colorItem.backgroundColour, foregroundName: colorItem.foregroundName, foregroundColour: colorItem.foregroundColour})
			);
		});
		return (
			React.createElement("div", {className: "swatch"}, 
				this.props.name, 
				colorNodes
			)
		);
	}
});

var SwatchesBox = React.createClass({displayName: "SwatchesBox",
	render: function() {
		var swatchNodes = this.props.swatches.map(function(swatch) {
			return (
				React.createElement(Swatch, {name: swatch.name, colors: swatch.colors})
			);
		});

		return (
			React.createElement("div", null, 
				swatchNodes
			)
		);
	}
});

React.render(
	React.createElement(SwatchesBox, {swatches: swatches}),
	document.getElementById('swatches')
	)