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


def main():
        
    fp = urllib.request.urlopen(sys.argv[1])

    url = sys.argv[1]

    options = Options()
    options.add_argument('headless')
    options.add_experimental_option('excludeSwitches', ['enable-logging'])
    options.add_argument("--start-fullscreen");
    options.add_argument("--start-maximized");

    browser = webdriver.Chrome(options=options)
    browser.get(url)
    time.sleep(2)

    try:
        element1 = browser.find_elements(By.XPATH, '//button')[2]
        element1.click()
    except Exception:
        pass
        
    try:
        element2 = browser.find_elements(By.XPATH, '//button[text()=\'Click to see nsfw\']')[0]
        element2.click()
    except Exception:
        pass
        

    doc = browser.page_source

    soup = BeautifulSoup(doc, "html.parser")
    post = soup.find_all("div", {"class": "Post"})
    head = soup.find_all("head")

    f = codecs.open("temp.html", "w", "utf-8")
    f.write("<html>" + str(head[0]) + "<div style=\"display: flex; justify-content: center; align-items: center; min-height: 85vh;\">" + str(post[0]) + "</div></html>")
    f.close()

    browser.set_window_size(650, 200)
    browser.get('file:///' + os.path.abspath("temp.html"))
    time.sleep(2)
    browser.execute_script("document.body.style.zoom='110%'")
    browser.execute_script("return document.body.style.overflow = 'hidden';")
    screenshot = browser.save_screenshot('screenshot.png')
    browser.quit()

    os.remove("temp.html")
    
    im = Image.open(r"screenshot.png").convert('RGB')
    newsize = (563, 173)
    im = im.resize(newsize)
    im = ImageOps.invert(im)
    im = im.save("screenshot.png")

    exit()

if __name__ == '__main__':
    main()
