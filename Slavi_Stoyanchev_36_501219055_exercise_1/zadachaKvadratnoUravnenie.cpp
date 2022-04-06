#include <iostream>
#include <cmath>
using namespace std;

int main() {

    float a, b, c, x1, x2, d;
    cout << "Please enter a, b and c: ";
    cin >> a >> b >> c;
    d = (b*b) - (4*a*c);
    
    if (d > 0) {
		cout << "Discriminant > 0" << endl;
        x1 = (-b + sqrt(d)) / (2*a);
        x2 = (-b - sqrt(d)) / (2*a);
        cout << "x1 = " << x1 << endl;
        cout << "x2 = " << x2 << endl;
    }
    
    else if (d == 0) {
        cout << "Discriminant = 0" << endl;
        x1 = -b/(2*a);
        cout << "x1,x2 = " << x1 << endl;
    }

    else {
		cout << "Discriminant < 0" ;
    }

    return 0;
}