var ResidenceInformation = React.createClass({
    hasAllInformationBeenGiven: function (loanInformation) {
        return loanInformation.amount &&
            loanInformation.value &&
            loanInformation.numberOfLoans &&
            loanInformation.value * 1.0 >= loanInformation.amount;
    },
    onInformationChange: function () {
        var amount = this.refs.loanAmount.value();
        var value = this.refs.valueOfHouse.value();

        var loanInformation = {
            amount: amount,
            value: value,
            numberOfLoans: ReactDOM.findDOMNode(this.refs.numberOfLoans).options[this.refs.numberOfLoans.selectedIndex].value,
            hasAllInformationBeenGiven: false
        };
        loanInformation.hasAllInformationBeenGiven = this.hasAllInformationBeenGiven(loanInformation);

        this.props.onInformationChange(loanInformation);
    },
    render: function () {
        return (
            <div key="loanInformation" className="row" id="residenceInformation">
                <div className="col-lg-9 col-md-11 col-md-offset-1 information-box">
                    <div className="row">
                        <div className="col-md-12 information-box-header">
                            <h4>Overordnet informationer om lån</h4>
                        </div>
                    </div>
                    <div className="row">
                        <div className="form-group col-sm-3">
                            <label htmlFor="loanAmount">Lånebeløb</label>
                            <CurrencyInput placeholder="1.500.000" 
                                           ref="loanAmount"
                                           onValueChange={this.onInformationChange} />
                        </div>
                    <div className="form-group col-sm-3">
                        <label htmlFor="valueOfHouse">Værdi af bolig</label>
                            <CurrencyInput placeholder="2.000.000"
                                           ref="valueOfHouse"
                                           onValueChange={this.onInformationChange} />
                    </div>
                    <div className="form-group col-sm-2">
                        <label htmlFor="numberOfLoans">Antal lån</label>
                        <select id="numberOfLoans" className="form-control" onChange={this.onInformationChange} ref="numberOfLoans">
                            <option>1</option>
                            <option>2</option>
                            <option>3</option>
                        </select>
                    </div>
                    </div>
                </div>
            </div>
        );
    }
});