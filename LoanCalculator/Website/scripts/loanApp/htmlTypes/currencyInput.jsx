var CurrencyInput = React.createClass({
    value: function() {
        return this.getPureNumber();
    },
    getPureNumber: function () {
        return parseInt(this.refs.input.value.replace(/\./g, ""));
    },
    setLocaleNumber: function (amount) {
        if (amount) {
            this.refs.input.value = amount.toLocaleString("da-dk");
        }
    },
    onSelect: function (e) {
        e.target.placeholder = "";
    },
    onDeselect: function (e) {
        e.target.placeholder = this.props.placeholder;
    },
    onChange: function (e) {
        e.preventDefault();

        var value = this.getPureNumber();
        this.setLocaleNumber(value);

        this.props.onValueChange();
    },
    render: function () {
        return (
            <div className="input-group">
                <input type="text"
                    min="100000"
                    step="50000"
                    pattern="[0-9.]*"
                    onBlur={this.onDeselect}
                    onSelect={this.onSelect}
                    onChange={this.onChange}
                    className="form-control text-right"
                    id="loanAmount"
                    placeholder={this.props.placeholder}
                    ref="input" />
                <span className="input-group-addon">kr.</span>
            </div>);
    }
});