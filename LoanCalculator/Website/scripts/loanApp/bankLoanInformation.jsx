var BankLoanInformation = React.createClass({
    getInitialState: function () {
        return {
            showMore: false
        };
    },
    onShowLess: function (e) {
        e.preventDefault();

        this.setState({
            showMore: false
        });
    },
    onShowMore: function (e) {
        e.preventDefault();

        this.setState({
            showMore: true
        });
    },
    render: function () {
        if (this.props.specificationCalculation === undefined)
            return false;

        var bankResult = this.props.specificationCalculation.bankResult;
        if (bankResult === undefined)
            return false;

        if (bankResult.bankPayout < 1000)
            return false;

        var text;
        if (this.state.showMore) { // TODO: The text below should be configurable with the ability to provide the payout value from the code
            text = (
                <div className="row">
                    <p>Banklån på <b>{bankResult.bankPayout.toLocaleString('da-dk', { style: 'currency', currency: 'DKK' })}</b> skal optages ved siden af realkreditlånet og medtages ikke i beregningerne for ydelsen, da denne kan varierere fra kunde til kunde.</p>
                    <p>Realkreditinstitutter tillader kun lån op til 80% af boligens værdi. Resten af beløbet skal enten hentes via et banklån eller som egenbetaling.</p>
                    <p>Da banker varierer deres rente på baggrund af egne vurderinger af din økonomi, så er det ikke muligt at stille en ydelse. Kontakt din egen bank og hent tilbud fra andre banker for at få en ide, om de vilkår de vil tilbyde dig på baggrund af din økonomi.</p>
                    <p>Det er som regel en god ide, at afbetale sit banklån så hurtigt som muligt. Det skyldes at banklån ofte vil have en højere rente eller dårligere vilkår end et realkreditlån.</p>
                    <a href="#" onClick={this.onShowLess}>Minimer</a>
                </div>
            );
        } else {
            text = (
                <div className="row">
                    <p>Banklån på <b>{bankResult.bankPayout.toLocaleString('da-dk', { style: 'currency', currency: 'DKK' })}</b> skal optages ved siden af realkreditlånet og medtages ikke i beregningerne for ydelsen, da denne kan varierere fra kunde til kunde. <a href="#" onClick={this.onShowMore}>Mere information</a></p>
                </div>
            );
        }

        return (
            <div key="bankLoan" className="row" id="bankLoan">
                <div className="col-lg-9 col-md-11 col-md-offset-1 information-box">
                    <div className="row">
                        <div className="col-md-12 information-box-header">
                                <h4>Banklån</h4>
                        </div>
                    </div>
                    <div className="col-md-12">
                        {text}
                    </div>
                </div>
            </div>
        );
    }
});