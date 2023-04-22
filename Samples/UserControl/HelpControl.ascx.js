
function HelpControl(clientID) {

    HelpControl.prototype.GetValue = function () {
      
        if (this.NullcheckBox != null)
            if (!this.NullcheckBox.checked)
                return this.valueTextBox.value;
            else
                return '';
        else
            return this.valueTextBox.value;
    }

    HelpControl.prototype.Enable = function (ClientID) {

        xvalueTextBox = document.getElementById(ClientID + "_txtHelp");
        xIDTextBox = document.getElementById(ClientID + "_hdHelpSelectID");
        xcheckBox = document.getElementById(ClientID + "_chkMakeNullHelp");
        ximgBox = document.getElementById(ClientID + "_imghelp");
        xDefaultTextBox = document.getElementById(ClientID + "_hdDefaultText");
        xDefaultIDBox = document.getElementById(ClientID + "_hdDefaultID");
        if (xcheckBox == null) return;
        if (!xcheckBox.checked) {
            xvalueTextBox.value = xDefaultTextBox.value;
            xIDTextBox.value = xDefaultIDBox.value;
            ximgBox.style.display = 'block';
//            alert(xvalueTextBox.width);
//            xvalueTextBox.width = xvalueTextBox.width - 15;
        }
        else {
            xDefaultTextBox.value = xvalueTextBox.value;
            xDefaultIDBox.value = xIDTextBox.value;
            xvalueTextBox.value = '';
            ximgBox.style.display = 'none';
//            xvalueTextBox.width = xvalueTextBox.width + 15;
        }


    }
    HelpControl.prototype.Reset = function (ClientID) {
        xvalueTextBox = document.getElementById(ClientID + "_txtHelp");
        xIDTextBox = document.getElementById(ClientID + "_hdHelpSelectID");
        xcheckBox = document.getElementById(ClientID + "_chkMakeNullHelp");
        xDefaultTextBox = document.getElementById(ClientID + "_hdDefaultText");
        xDefaultIDBox = document.getElementById(ClientID + "_hdDefaultID");
       
        xvalueTextBox.value = '';
        xIDTextBox.value = '!~';
        xDefaultTextBox.value = '';
        xDefaultIDBox.value = '';
        
    }
    this.clientID = clientID;
    this.valueTextBox = document.getElementById(clientID + "_hdHelpSelectID");
    this.imgHelp = document.getElementById(clientID + "_imgHelp");
    this.imgReset = document.getElementById(clientID + "_imgReset");
    this.defaultValueBox = document.getElementById(clientID + "_hdDefaultText");
    this.defaultIDBox = document.getElementById(clientID + "_hdDefaultID");
    this.NullcheckBox = document.getElementById(clientID + "_chkMakeNullHelp");
}

