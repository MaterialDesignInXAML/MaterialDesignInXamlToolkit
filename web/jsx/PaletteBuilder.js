var Color = React.createClass({
	render: function() {

		var style = {
			background:this.props.backgroundColour,
			color:this.props.foregroundColour
		};

		return (
			<div style={style}>{this.props.backgroundName}</div>
		);
	}
});

var Swatch = React.createClass({
	render: function() {
		var colorNodes = this.props.colors.map(function (colorItem) {
			return (
				<Color backgroundName={colorItem.backgroundName} backgroundColour={colorItem.backgroundColour} foregroundName={colorItem.foregroundName} foregroundColour={colorItem.foregroundColour} />
			);
		});
		return (
			<div className="swatch">
				{this.props.name}
				{colorNodes}
			</div>
		);
	}
});

var SwatchesBox = React.createClass({
	render: function() {
		var swatchNodes = this.props.swatches.map(function(swatch) {
			return (
				<Swatch name={swatch.name} colors={swatch.colors} />
			);
		});

		return (
			<div>
				{swatchNodes}
			</div>
		);
	}
});

React.render(
	<SwatchesBox swatches={swatches} />,
	document.getElementById('swatches')
	)