from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.chrome.options import Options
from bs4 import BeautifulSoup
from PIL import Image, ImageOps
import sys
import urllib.request
import codecs
import os
import time
import re
import json


def main():
    
    with open('../cursewords.json') as json_file:
        curse = json.load(json_file)
        
    url = sys.argv[1]
    
    quantity = int(sys.argv[2]) * 5
    

    options = Options()
    options.add_argument('headless')
    options.add_experimental_option('excludeSwitches', ['enable-logging'])
    options.add_argument("--start-fullscreen");
    options.add_argument("--start-maximized");

    browser = webdriver.Chrome(options=options)
    browser.get(url)
    time.sleep(2)

    try:
        element = browser.find_elements(By.XPATH, '//button')[2]
        element.click()
    except Exception:
        pass
    
    time.sleep(10)
    doc = browser.page_source

    soup = BeautifulSoup(doc, "html.parser")
    comments = soup.find_all("div", {"class": "Comment"})
    head = soup.find_all("head")
    
    count = 0
    
    for i in range(0, len(comments)):
        if re.search("<span class=.*>level 1</span>", str(comments[i])) != None:
        
            if count == quantity:
                break
        
            f1 = codecs.open("temp.html", "w", "utf-8")
            f1.write("<html>" + str(head[0]) + "<div style=\"display: flex; justify-content: center; align-items: center; min-height: 85vh;\">" + str(comments[i]) + "</div></html>")
            f1.close()

            browser.set_window_size(650, 300)
            browser.get('file:///' + os.path.abspath("temp.html"))
            time.sleep(2)
            
            comment = browser.find_elements(By.XPATH, '//div[contains(@class, \'Comment\')]//p')
            text = ''.join([x.get_attribute('innerText') + '\n' for x in comment])
            
            browser.execute_script("return document.body.style.overflow = 'hidden';")
            
            if len(text) < 100:
                browser.execute_script("document.body.style.zoom='150%'")
            elif len(text) > 1200:
                browser.execute_script("document.body.style.zoom='50%'")
            elif len(text) > 800:
                browser.execute_script("document.body.style.zoom='60%'")
            elif len(text) > 400:
                browser.execute_script("document.body.style.zoom='70%'")
            
            screenshot = browser.save_screenshot(str(count) + 'screenshot.png')
            
            browser.execute_script("document.body.style.zoom='100%'")
            
            f2 = codecs.open(str(count) + ".txt", "w", "utf-8")
            for word in curse:
                text = text.replace(word, curse[word])
            f2.write(text + '\n')
            f2.close()

            os.remove("temp.html")
            
            im = Image.open(str(count) + "screenshot.png").convert('RGB')
            newsize = (563, 260)
            im = im.resize(newsize)
            
            im = ImageOps.invert(im)
            im = im.save(str(count) + "screenshot.png")
            
            count += 1

    browser.quit()
    exit()

if __name__ == '__main__':
    main()
