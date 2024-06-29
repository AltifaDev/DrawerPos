// Orders.js
window.getBalanceValue = function () {
    return document.getElementById('balance-display').value;
}

function formatCurrency(value) {
    let formattedValue = parseFloat(value).toLocaleString('th-TH', {
        style: 'decimal',
        minimumFractionDigits: 0,
    });
    return formattedValue + ' ฿';
}

function NotCalculator() {
    Swal.fire({
        icon: "error",
        title: "ไม่พบรายการสั่งซื้อ...",
        text: "กรุณาเลือกสินค้า ก่อนชำระเงิน!",
    });
}

function showNumber(amountDue) {
    Swal.fire({
        html: `
            <div class="rounded-lg shadow-md p-4 w-full bg-white">
                <div class="flex justify-between items-center mb-6">
                    <button class="text-xl font-bold" onclick="Swal.close()">X</button>
                    <h2 class="text-lg font-semibold">ยอดเงินที่ต้องชำระ ${amountDue}</h2>
                    <span></span>
                </div>
                <input id="balancedisplay" type="text" class="bg-gray-100 text-center text-blue-500 text-xl font-bold py-4 border-gray-300 mb-6 w-full" readonly value="0 ฿"/>
                <div class="grid grid-cols-4 gap-2">
                    <button class="bg-gray-200 py-4 rounded text-xl font-semibold" onclick="buttonClicked(7)">7</button>
                    <button class="bg-gray-200 py-4 rounded text-xl font-semibold" onclick="buttonClicked(8)">8</button>
                    <button class="bg-gray-200 py-4 rounded text-xl font-semibold" onclick="buttonClicked(9)">9</button>
                    <button class="bg-gray-200 py-4 rounded text-xl font-semibold" onclick="buttonClicked(100)">100</button>
                    <button class="bg-gray-200 py-4 rounded text-xl font-semibold" onclick="buttonClicked(4)">4</button>
                    <button class="bg-gray-200 py-4 rounded text-xl font-semibold" onclick="buttonClicked(5)">5</button>
                    <button class="bg-gray-200 py-4 rounded text-xl font-semibold" onclick="buttonClicked(6)">6</button>
                    <button class="bg-gray-200 py-4 rounded text-xl font-semibold" onclick="buttonClicked(500)">500</button>
                    <button class="bg-gray-200 py-4 rounded text-xl font-semibold" onclick="buttonClicked(1)">1</button>
                    <button class="bg-gray-200 py-4 rounded text-xl font-semibold" onclick="buttonClicked(2)">2</button>
                    <button class="bg-gray-200 py-4 rounded text-xl font-semibold" onclick="buttonClicked(3)">3</button>
                    <button class="bg-gray-200 py-4 rounded text-xl font-semibold" onclick="buttonClicked(1000)">1000</button>
                    <button class="bg-gray-200 py-4 rounded text-xl font-semibold" onclick="buttonClicked('00')">00</button>
                    <button class="bg-gray-200 py-4 rounded text-xl font-semibold" onclick="buttonClicked(0)">0</button>
                    <button class="bg-gray-200 py-4 rounded text-xl font-semibold" onclick="buttonClicked('back')">&lt;</button>
                    <button class="bg-red-200 py-4 rounded text-xl font-semibold text-red-600" onclick="buttonClicked('C')">C</button>
                </div>
                <button id="open-drawer-button" type="button" class="bg-blue-500 text-white py-2 mt-6 w-full rounded-lg flex items-center justify-center" data-total="${amountDue}">
                    <svg fill="#000000" width="30px" height="30px" viewBox="0 0 1024 1024" xmlns="http://www.w3.org/2000/svg"><path style="fill:#FFFFFF;" d="M1022.98 509.984L905.475 102.895c-3.84-13.872-16.464-23.472-30.848-23.472H139.283c-14.496 0-27.184 9.744-30.944 23.777L.947 489.552c-1.984 7.504-1.009 15.007 1.999 21.536C1.218 516.88.003 522.912.003 529.264v351.312c0 35.343 28.656 64 64 64h896c35.343 0 64-28.657 64-64V529.264c0-1.712-.369-3.329-.496-5.008.832-4.592.816-9.44-.527-14.272zm-859.078-366.56l686.369-.001 93.12 321.84H645.055c-1.44 76.816-55.904 129.681-133.057 129.681s-130.624-52.88-132.064-129.68H74.158zm796.097 737.151H64.001V529.263h263.12c27.936 80.432 95.775 129.68 184.879 129.68s157.936-49.248 185.871-129.68h262.128v351.312z"/></svg>
                    <span>&nbsp;Open Drawer</span>
                </button>
            </div>
        `,
        showConfirmButton: false,
        didOpen: () => {
            document.getElementById('open-drawer-button').onclick = function () {
                const amountDue = parseFloat(this.getAttribute('data-total').replace(/[^\d.-]/g, ''));
                callUpdateBalanceView(amountDue);
                updateBalanceView();
                DotNet.invokeMethodAsync('DrawerPos.Blazor', 'HandleValidSubmit');
            };

            window.buttonClicked = (num) => {
                const display = document.getElementById('balance-display');
                const displayCal = document.getElementById('balancedisplay');
                let currentValue = display.value.replace('฿', '').replace(/,/g, '').trim();

                if (num === 'C') {
                    currentValue = '0';
                } else if (num === 'back') {
                    currentValue = currentValue.slice(0, -1) || '0';
                } else if (num === 100 || num === 500 || num === 1000) {
                    currentValue = (parseFloat(currentValue) || 0) + num;
                } else {
                    if (currentValue === '0') {
                        currentValue = '';
                    }
                    currentValue += num;
                }

                if (!isNaN(currentValue) && currentValue !== '') {
                    currentValue = parseFloat(currentValue);
                } else {
                    currentValue = 0;
                }

                let formattedValue = formatCurrency(currentValue);
                display.value = formattedValue;
                displayCal.value = formattedValue;
            }
        }
    });
}

function updateBalanceView() {
    const balanceDisplay = document.getElementById('balance-display').value;
    const paymentMethod = "Cash";

    document.getElementById('balance-displayview').value = balanceDisplay;
    document.getElementById('payment-method').value = paymentMethod;
}

function callUpdateBalanceView(amountDue) {
    const balanceDisplay = document.getElementById('balance-display');
    let balanceValue = balanceDisplay.value.replace('฿', '').replace(/,/g, '').trim();
    balanceValue = parseFloat(balanceValue);

    if (balanceValue >= amountDue) {
        Swal.fire({
            icon: "success",
            title: "ชำระเงินสำเร็จ!",
            text: `เงินทอน ${formatCurrency(balanceValue - amountDue)}`,
        });
    } else {
        Swal.fire({
            icon: "error",
            title: "ยอดเงินไม่เพียงพอ",
            text: `ยอดเงินที่ต้องชำระ ${formatCurrency(amountDue)} แต่คุณมี ${formatCurrency(balanceValue)}`
        });
    }

    DotNet.invokeMethodAsync('DrawerPos.Blazor', 'StaticUpdateBalanceView');
}
