import sys
import json
from datetime import datetime, timedelta

def main():
    if len(sys.argv) < 2:
        print("Error: No file provided.")
        return
    
    file_path = sys.argv[1]

    with open(file_path, 'r', encoding='utf-8') as f:
        gold_prices = json.load(f)

    last_year = datetime.now() - timedelta(days=365)
    last_year_str = last_year.strftime('%Y-%m-%dT%H:%M:%S')
    
    filtered = [p for p in gold_prices if p['Date'] >= last_year_str]
    
    sorted_prices = sorted(filtered, key=lambda x: x['Price'], reverse=True)
    top_3 = sorted_prices[:3]

    print("\n--- Python Solution (List Comprehension) ---")
    for item in top_3:
        short_date = item['Date'].split('T')[0]
        print(f"{short_date} - {item['Price']} PLN")

if __name__ == "__main__":
    main()