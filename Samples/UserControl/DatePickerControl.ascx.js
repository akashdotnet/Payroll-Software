function DatePickerControl(clientID,_Enabled) {

    DatePickerControl.prototype.GetValue = function () {
        return this.valueTextBox.value;
    }
    DatePickerControl.prototype.GetClientValue = function (ClientID) {
        xvalueTextBox = document.getElementById(ClientID + "_txtDate");
        return xvalueTextBox.value;
    }

    DatePickerControl.prototype.Enable = function (ClientID) {

        xvalueTextBox = document.getElementById(ClientID + "_txtDate");
        xcheckBox = document.getElementById(ClientID + "_chkMakeNull");
        ximgBox = document.getElementById(ClientID + "_imgCal");
        xDefaultTextBox = document.getElementById(ClientID + "_hdDefault");
        
        if (xcheckBox == null) return;
        if (xcheckBox.checked) {
            xvalueTextBox.value = xDefaultTextBox.value;
            ximgBox.style.display = 'block';
            xvalueTextBox.disabled = false;
        }
        else {
            xvalueTextBox.disabled = true;
            xvalueTextBox.value = '';
            ximgBox.style.display = 'none';
        }


    }
    DatePickerControl.prototype.ClientEnable = function (value) {
        if (value == 'true') {
           // alert('true');
            this.valueTextBox.disabled = false;
            if (this.checkBox != null)
                this.checkBox.disabled = false;
            if (this.imgBox != null)
                this.imgBox.style.display = 'block';
        }
        else {
            this.valueTextBox.disabled = true;
            if (this.checkBox != null)
                this.checkBox.disabled = true;
            if (this.imgBox != null)
                this.imgBox.style.display = 'none';
        }

    }
    DatePickerControl.prototype.SetDate = function (ClientID, xDate) {

      
        var xvalueTextBox = document.getElementById(ClientID + "_txtDate");
        var xDefaultTextBox = document.getElementById(ClientID + "_hdDefault");
        xvalueTextBox.value = xDate;
        xDefaultTextBox.value = xDate;
    }
    DatePickerControl.prototype.CheckDate = function (ClientID, DateFormat) {

        DateFormat = DateFormat.replace('MON', 'MMM');
        DateFormat = DateFormat.replace('DD', 'dd');
        DateFormat = DateFormat.replace('YYYY', 'yyyy');
        DateFormat = DateFormat.replace('/', '');
        DateFormat = DateFormat.replace("\\", "");
        DateFormat = DateFormat.replace('-', '');
        DateFormat = DateFormat.replace('/', '');
        DateFormat = DateFormat.replace("\\", "");
        DateFormat = DateFormat.replace('-', '');

        var dtFmt = ''

        switch (DateFormat) {
            case 'ddMMyyyy':
                dtFmt = 'W';
                break;
            case 'ddMMMyyyy':
                dtFmt = 'W';
                break;
            case 'MMddyyyy':
                dtFmt = 'U';
                break;
            case 'MMMddyyyy':
                dtFmt = 'U';
                break;

            case 'yyyyMMdd':
                dtFmt = 'J';
                break;
            case 'yyyyMMMdd':
                dtFmt = 'J';
                break;


        }

        xvalueTextBox = document.getElementById(ClientID + "_txtDate");
        xDefaultTextBox = document.getElementById(ClientID + "_hdDefault");
        var xcheckBox = document.getElementById(ClientID + "_chkMakeNull");
       
        if (validateDate(xvalueTextBox.value, dtFmt, 'A')) {

            xDefaultTextBox.value = xvalueTextBox.value;
            if (xcheckBox != null)
                xcheckBox.checked = true;
        }
        else {
            if (xcheckBox != null)
                xcheckBox.checked = false;
            xvalueTextBox.value = '';
            xDefaultTextBox.value = '';
        }
    }
    this.enabled = _Enabled;
    
    this.clientID = clientID;
    this.valueTextBox = document.getElementById(clientID + "_txtDate");
    this.checkBox = document.getElementById(clientID + "_chkMakeNull");
    this.imgBox = document.getElementById(clientID + "_imgCal");
    this.defaultValueBox = document.getElementById(clientID + "_hdDefault");
    this.ParentPanel = document.getElementById(clientID + "_pnlDtp");

    if (this.enabled=='false') {
        xvalueTextBox = document.getElementById(clientID + "_txtDate");
        xcheckBox = document.getElementById(clientID + "_chkMakeNull");
        ximgBox = document.getElementById(clientID + "_imgCal");
        xcheckBox.checked = false;
        xvalueTextBox.disabled = !xvalueTextBox.disabled;
        xvalueTextBox.value = '';
        ximgBox.style.display = 'none';
    }


//    validateDate(myform.mydate.value,valDateFmt(myform.datefmt), 'A')

   
    function stripBlanks(fld) {
        var result = ""; var c = 0; for (i = 0; i < fld.length; i++) {
            if (fld.charAt(i) != " " || c > 0) {
                result += fld.charAt(i);
                if (fld.charAt(i) != " ") c = result.length;
            } 
        } return result.substr(0, c);
    }
    var numb = '0123456789';
    function isValid(parm, val) {
        if (parm == "") return true;
        for (i = 0; i < parm.length; i++) {
            if (val.indexOf(parm.charAt(i), 0) == -1)
                return false;
        } return true;
    }
    function isNumber(parm) { return isValid(parm, numb); }
    var mth = new Array(' ', 'january', 'february', 'march', 'april', 'may', 'june', 'july', 'august', 'september', 'october', 'november', 'december');
    var day = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
    function validateDate(fld, fmt, rng) {
        var spliter = '/';
        
        var dd, mm, yy; var today = new Date; var t = new Date; fld = stripBlanks(fld);
        if (fld == '') return false;

        var d1 = fld.split('\/');

        if (d1.length < 2) {
            d1 = fld.split('-');
            spliter = '-';
        }
        if (d1.length != 3) d1 = fld.split(' ');
        if (d1.length != 3) return false;
       
        if (fmt == 'u' || fmt == 'U') {
            dd = d1[1]; mm = d1[0]; yy = d1[2];
        }
        else if (fmt == 'j' || fmt == 'J') {
            dd = d1[2]; mm = d1[1]; yy = d1[0];
        }
        else if (fmt == 'w' || fmt == 'W') {
            dd = d1[0]; mm = d1[1]; yy = d1[2];
        }
        else return false;
       
        var n = dd.lastIndexOf('st');
        if (n > -1) dd = dd.substr(0, n);
        n = dd.lastIndexOf('nd');
        if (n > -1) dd = dd.substr(0, n);
        n = dd.lastIndexOf('rd');
        if (n > -1) dd = dd.substr(0, n);
        n = dd.lastIndexOf('th');
        if (n > -1) dd = dd.substr(0, n);
        n = dd.lastIndexOf(',');
        if (n > -1) dd = dd.substr(0, n);
        n = mm.lastIndexOf(',');
        if (n > -1) mm = mm.substr(0, n);

      
        if (!isNumber(dd)) return false;
        if (!isNumber(yy)) return false;
        if (!isNumber(mm)) {
            var nn = mm.toLowerCase();
            for (var i = 1; i < 13; i++) {
                if (nn == mth[i] ||
        nn == mth[i].substr(0, 3)) { mm = i; i = 13; }
            }
        }

        if (!isNumber(mm)) return false;
      
        dd = parseFloat(dd); mm = parseFloat(mm); yy = parseFloat(yy);
        if (yy < 100) yy += 2000;
        if (yy < 1582 || yy > 4881) return false;
        
        if (mm == 2 && (yy % 400 == 0 || (yy % 4 == 0 && yy % 100 != 0))) day[1] = 29; else day[1] = 28;
        if (mm < 1 || mm > 12) return false;
       
        if (dd < 1 || dd > day[mm - 1]) return false;
        t.setDate(dd); t.setMonth(mm - 1); t.setFullYear(yy);
       
        if (rng == 'p' || rng == 'P') {
            
            if (t > today) return false;
        }

        else if (rng == 'f' || rng == 'F') {
           
            if (t < today) return false;
        }
        else if (rng != 'a' && rng != 'A') return false;
     
        return true;
    }

}

